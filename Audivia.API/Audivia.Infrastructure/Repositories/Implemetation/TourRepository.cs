using Audivia.Domain.ModelRequests.Statistics;
using Audivia.Domain.ModelResponses.Statistics;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class TourRepository : BaseRepository<Tour>, ITourRepository
    {
        public TourRepository(IMongoDatabase database) : base(database)
        {

        }

        public async Task<List<Tour>> GetToursByTypesExcludingIdsAsync(List<string> tourTypes, List<string> excludeTourIds)
        {
            var filter = Builders<Tour>.Filter.In(t => t.TourType.TourTypeName, tourTypes) &
                         Builders<Tour>.Filter.Nin(t => t.Id, excludeTourIds);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<List<TourStatItem>> GetTourStatisticsAsync(GetTourStatRequest request)
        {
            var pipeline = new List<BsonDocument>
            {
                // Exclude deleted tours
                new BsonDocument("$match", new BsonDocument("is_deleted", false))
            };

            switch (request.GroupBy?.ToLower())
            {
                case "tour_type":
                    pipeline.AddRange(BuildTourTypeGroupPipeline());
                    break;
                case "rating_group":
                    pipeline.AddRange(BuildRatingGroupPipeline());
                    break;
                default:
                    // Return empty list if GroupBy is not supported
                    return new List<TourStatItem>();
            }

            return await _collection.Aggregate<TourStatItem>(pipeline).ToListAsync();
        }

        private static IEnumerable<BsonDocument> BuildTourTypeGroupPipeline()
        {
            return new BsonDocument[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", "$tour_type.tour_type_name" },
                    { "Value", new BsonDocument("$sum", 1) }
                }),
                new BsonDocument("$project", new BsonDocument
                {
                    { "_id", 0 },
                    { "GroupKey", "$_id" },
                    { "Value", "$Value" }
                }),
                new BsonDocument("$sort", new BsonDocument("Value", -1))
            };
        }

        private static IEnumerable<BsonDocument> BuildRatingGroupPipeline()
        {
            return new BsonDocument[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", new BsonDocument("$switch", new BsonDocument
                        {
                            { "branches", new BsonArray
                                {
                                    new BsonDocument
                                    {
                                        { "case", new BsonDocument("$lt", new BsonArray { "$avg_rating", 2 }) },
                                        { "then", "< 2 sao" }
                                    },
                                    new BsonDocument
                                    {
                                        { "case", new BsonDocument("$and", new BsonArray
                                            {
                                                new BsonDocument("$gte", new BsonArray { "$avg_rating", 2 }),
                                                new BsonDocument("$lt", new BsonArray { "$avg_rating", 3 })
                                            })
                                        },
                                        { "then", "2 - 3 sao" }
                                    },
                                    new BsonDocument
                                    {
                                        { "case", new BsonDocument("$and", new BsonArray
                                            {
                                                new BsonDocument("$gte", new BsonArray { "$avg_rating", 3 }),
                                                new BsonDocument("$lt", new BsonArray { "$avg_rating", 4 })
                                            })
                                        },
                                        { "then", "3 - 4 sao" }
                                    },
                                    new BsonDocument
                                    {
                                        { "case", new BsonDocument("$gte", new BsonArray { "$avg_rating", 4 }) },
                                        { "then", "4 - 5 sao" }
                                    }
                                }
                            },
                            { "default", "Chưa được đánh giá" }
                        })
                    },
                    { "Value", new BsonDocument("$sum", 1) }
                }),
                new BsonDocument("$project", new BsonDocument
                {
                    { "_id", 0 },
                    { "GroupKey", "$_id" },
                    { "Value", "$Value" }
                }),
                new BsonDocument("$sort", new BsonDocument("GroupKey", 1))
            };
        }
    }
}
