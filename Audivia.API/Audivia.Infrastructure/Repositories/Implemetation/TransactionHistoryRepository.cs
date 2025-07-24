using Audivia.Domain.ModelRequests.Statistics;
using Audivia.Domain.ModelResponses.Statistics;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class TransactionHistoryRepository : BaseRepository<TransactionHistory>, ITransactionHistoryRepository
    {
        public TransactionHistoryRepository(IMongoDatabase database) : base(database) {
        }
        public async Task<List<TransactionHistory>> GetTransactionHistoryByUserId(string userId)
        {
            var objectId = new ObjectId(userId);
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument("user_id", objectId)),
                new BsonDocument("$sort", new BsonDocument("created_at", -1)),
                new BsonDocument("$lookup", new BsonDocument
                {
                    {"from", "Tour" },
                    { "localField", "tour_id" },
                    { "foreignField", "_id" },
                    { "as", "Tour" }
                }),
                 new BsonDocument("$unwind", "$Tour")


            };
            var rs = await _collection.Aggregate<TransactionHistory>(pipeline).ToListAsync();
            return rs;
        }

        public async Task<Dictionary<string, int>> GetTopTourTypesByUserIdAsync(string userId, int topN)
        {
            var userObjectId = new ObjectId(userId);

            var pipeline = new BsonDocument[]
            {
            new BsonDocument("$match", new BsonDocument("user_id", userObjectId)),
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "Tour" },
                { "localField", "tour_id" },
                { "foreignField", "_id" },
                { "as", "Tour" }
            }),
            new BsonDocument("$unwind", "$Tour"),
            new BsonDocument("$group", new BsonDocument
            {
                { "_id", "$Tour.tour_type.tour_type_name" },
                { "count", new BsonDocument("$sum", 1) }
            }),
            new BsonDocument("$sort", new BsonDocument("count", -1)),
            new BsonDocument("$limit", topN)
            };

            var result = await _collection.Aggregate<BsonDocument>(pipeline).ToListAsync();
            return result.ToDictionary(r => r["_id"].AsString, r => r["count"].ToInt32());
        }

        public async Task<List<string?>> GetBookedTourIdsByUserIdAsync(string userId)
        {
            return await _collection
                .Find(t => t.UserId == userId)
                .Project(t => t.TourId)
                .ToListAsync();
        }

        public async Task<TransactionHistory> GetTransactionHistoryByUserIdAndTourId(string userId, string tourId)
        {
            return await _collection.Find(x => x.UserId == userId && x.TourId == tourId).FirstOrDefaultAsync();
        }

        public async Task<List<RevenueStatItem>> GetRevenueStatisticsAsync(GetRevenueStatRequest request)
        {
            var pipeline = new List<BsonDocument>();

            // $match stage: Filter by date range
            var matchFilter = new BsonDocument();
            if (request.StartDate.HasValue)
            {
                var startDate = request.StartDate.Value.ToDateTime(TimeOnly.MinValue);
                matchFilter.Add("created_at", new BsonDocument("$gte", new BsonDateTime(startDate)));
            }
            if (request.EndDate.HasValue)
            {
                var endDate = request.EndDate.Value.ToDateTime(TimeOnly.MaxValue);
                var dateFilter = matchFilter.GetValue("created_at", new BsonDocument()).AsBsonDocument;
                dateFilter.Add("$lte", new BsonDateTime(endDate));
                matchFilter["created_at"] = dateFilter;
            }

            if (matchFilter.ElementCount > 0)
            {
                pipeline.Add(new BsonDocument("$match", matchFilter));
            }

            // $group stage: Group by the specified criteria
            BsonDocument groupStage;
            string groupByKeySort = "GroupKey";

            switch (request.GroupBy?.ToLower())
            {
                case "day":
                    groupStage = BuildGroupByDate("day", "%Y-%m-%d");
                    pipeline.Add(groupStage);
                    break;
                case "month":
                    groupStage = BuildGroupByDate("month", "%Y-%m");
                    pipeline.Add(groupStage);
                    break;
                case "year":
                    groupStage = BuildGroupByDate("year", "%Y");
                    pipeline.Add(groupStage);
                    break;
                case "tour":
                    pipeline.Add(new BsonDocument("$lookup", new BsonDocument
                    {
                        { "from", "Tour" },
                        { "localField", "tour_id" },
                        { "foreignField", "_id" },
                        { "as", "TourInfo" }
                    }));
                    pipeline.Add(new BsonDocument("$unwind", "$TourInfo"));
                    pipeline.Add(new BsonDocument("$group", new BsonDocument
                    {
                        { "_id", "$TourInfo.title" },
                        { "revenue", new BsonDocument("$sum", "$amount") }
                    }));
                    break;
                case "user_age_group":
                    pipeline.AddRange(BuildUserAgeGroupPipeline());
                    groupByKeySort = "_id"; // The group key is now _id
                    break;
                default:
                    // Default to grouping by month if not specified or invalid
                    groupStage = BuildGroupByDate("month", "%Y-%m");
                    pipeline.Add(groupStage);
                    break;
            }

            // Add sorting by revenue before projection
            pipeline.Add(new BsonDocument("$sort", new BsonDocument("revenue", -1)));

            // $limit stage
            if (request.Top.HasValue && request.Top > 0)
            {
                pipeline.Add(new BsonDocument("$limit", request.Top.Value));
            }
            
            // $project stage to shape the output
            pipeline.Add(new BsonDocument("$project", new BsonDocument
            {
                { "_id", 0 },
                { "GroupKey", "$_id" },
                { "Revenue", "$revenue" }
            }));

            var finalSort = new BsonDocument("$sort", new BsonDocument(groupByKeySort, 1));
            if (request.GroupBy?.ToLower() == "tour" || (request.Top.HasValue && request.Top > 0)) {
            } else {
                pipeline.Add(finalSort);
            }

            return await _collection.Aggregate<RevenueStatItem>(pipeline).ToListAsync();
        }

        private BsonDocument BuildGroupByDate(string unit, string format)
        {
             return new BsonDocument("$group", new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString", new BsonDocument { { "format", format }, { "date", "$created_at" } }) },
                { "revenue", new BsonDocument("$sum", "$amount") }
            });
        }

        private IEnumerable<BsonDocument> BuildUserAgeGroupPipeline()
        {
            return new BsonDocument[]
            {
                // Join with User collection
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "User" },
                    { "localField", "user_id" },
                    { "foreignField", "_id" },
                    { "as", "UserInfo" }
                }),
                new BsonDocument("$unwind", "$UserInfo"),
                
                // Filter out users without a birthday
                new BsonDocument("$match", new BsonDocument("UserInfo.birth_day", new BsonDocument("$ne", BsonNull.Value))),

                // Calculate age and determine age group
                new BsonDocument("$addFields", new BsonDocument
                {
                    { "age", new BsonDocument("$dateDiff", new BsonDocument
                        {
                            { "startDate", "$UserInfo.birth_day" },
                            { "endDate", "$$NOW" },
                            { "unit", "year" }
                        })
                    }
                }),
                new BsonDocument("$addFields", new BsonDocument
                {
                    { "ageGroup", new BsonDocument("$switch", new BsonDocument
                        {
                            { "branches", new BsonArray
                                {
                                    new BsonDocument
                                    {
                                        { "case", new BsonDocument("$lte", new BsonArray { "$age", 18 }) },
                                        { "then", "Under 18" }
                                    },
                                    new BsonDocument
                                    {
                                        { "case", new BsonDocument("$lte", new BsonArray { "$age", 25 }) },
                                        { "then", "18-25" }
                                    },
                                    new BsonDocument
                                    {
                                        { "case", new BsonDocument("$lte", new BsonArray { "$age", 35 }) },
                                        { "then", "26-35" }
                                    },
                                    new BsonDocument
                                    {
                                        { "case", new BsonDocument("$lte", new BsonArray { "$age", 50 }) },
                                        { "then", "36-50" }
                                    },
                                    new BsonDocument
                                    {
                                        { "case", new BsonDocument("$gt", new BsonArray { "$age", 50 }) },
                                        { "then", "Over 50" }
                                    }
                                }
                            },
                            { "default", "Unknown" }
                        })
                    }
                }),

                // Group by age group and sum revenue
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", "$ageGroup" },
                    { "revenue", new BsonDocument("$sum", "$amount") }
                })
            };
        }

        public async Task<List<TourWithPurchaseCount>> GetTopPurchasedToursWithDetailAsync(int topN)
        {
            var pipeline = new BsonDocument[]
                {
                    new BsonDocument("$match", new BsonDocument("is_deleted", false)),

                    // Nhóm theo tour_id, đếm số lượng mua
                    new BsonDocument("$group", new BsonDocument
                    {
                        { "_id", "$tour_id" },
                        { "PurchaseCount", new BsonDocument("$sum", 1) }
                    }),

                    // Sort giảm dần
                    new BsonDocument("$sort", new BsonDocument("PurchaseCount", -1)),

                    // Limit top N
                    new BsonDocument("$limit", topN),

                    // Join với collection Tour
                    new BsonDocument("$lookup", new BsonDocument
                    {
                        { "from", "Tour" },
                        { "localField", "_id" },
                        { "foreignField", "_id" },
                        { "as", "Tour" }
                    }),

                    // Unwind để lấy 1 tour
                    new BsonDocument("$unwind", "$Tour"),

                    // Project thông tin Tour + purchaseCount
                    new BsonDocument("$project", new BsonDocument
                    {
                        { "_id", 0 },
                        { "PurchaseCount", 1 },
                        { "Tour", "$Tour" }
                    })
                };

                return await _collection.Aggregate<TourWithPurchaseCount>(pipeline).ToListAsync();
        }

    }
}

