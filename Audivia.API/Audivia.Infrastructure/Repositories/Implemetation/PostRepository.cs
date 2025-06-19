using Audivia.Domain.ModelRequests.Statistics;
using Audivia.Domain.ModelResponses.Statistics;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(IMongoDatabase database) : base(database) { }

        public async Task<List<PostStatItem>> GetPostStatisticsAsync(GetPostStatRequest request)
        {
            var pipeline = new List<BsonDocument>();
            var matchFilter = new BsonDocument("is_deleted", false);

            if (request.StartDate.HasValue)
            {
                matchFilter.Add("created_at", new BsonDocument("$gte", new BsonDateTime(request.StartDate.Value.ToDateTime(TimeOnly.MinValue))));
            }
            if (request.EndDate.HasValue)
            {
                var dateFilter = matchFilter.GetValue("created_at", new BsonDocument()).AsBsonDocument;
                dateFilter.Add("$lte", new BsonDateTime(request.EndDate.Value.ToDateTime(TimeOnly.MaxValue)));
                matchFilter["created_at"] = dateFilter;
            }
            pipeline.Add(new BsonDocument("$match", matchFilter));

            var groupStage = BuildPostGroupStage(request.GroupBy);
            if(groupStage != null)
            {
                pipeline.AddRange(groupStage);
            }
            
            pipeline.Add(new BsonDocument("$sort", new BsonDocument("GroupKey", 1)));

            return await _collection.Aggregate<PostStatItem>(pipeline).ToListAsync();
        }
        
        private IEnumerable<BsonDocument> BuildPostGroupStage(string groupBy)
        {
            switch (groupBy?.ToLower())
            {
                case "day":
                case "month":
                case "year":
                    string format = groupBy.ToLower() == "day" ? "%Y-%m-%d" : (groupBy.ToLower() == "month" ? "%Y-%m" : "%Y");
                    return new BsonDocument[]
                    {
                        new BsonDocument("$group", new BsonDocument
                        {
                            { "_id", new BsonDocument("$dateToString", new BsonDocument { { "format", format }, { "date", "$created_at" } }) },
                            { "Value", new BsonDocument("$sum", 1) }
                        }),
                        new BsonDocument("$project", new BsonDocument { { "GroupKey", "$_id" }, { "Value", "$Value" }, { "_id", 0 } })
                    };
                case "user_age_group":
                    return BuildPostUserAgeGroupPipeline();
                default:
                   return null;
            }
        }
        
        private IEnumerable<BsonDocument> BuildPostUserAgeGroupPipeline()
        {
            return new BsonDocument[]
            {
                new BsonDocument("$lookup", new BsonDocument { { "from", "User" }, { "localField", "user_id" }, { "foreignField", "_id" }, { "as", "UserInfo" } }),
                new BsonDocument("$unwind", "$UserInfo"),
                new BsonDocument("$match", new BsonDocument("UserInfo.birth_day", new BsonDocument("$ne", BsonNull.Value))),
                new BsonDocument("$addFields", new BsonDocument
                {
                    { "age", new BsonDocument("$dateDiff", new BsonDocument { { "startDate", "$UserInfo.birth_day" }, { "endDate", "$$NOW" }, { "unit", "year" } }) }
                }),
                new BsonDocument("$addFields", new BsonDocument
                {
                    { "ageGroup", new BsonDocument("$switch", new BsonDocument
                        {
                            { "branches", new BsonArray
                                {
                                    new BsonDocument { { "case", new BsonDocument("$lte", new BsonArray { "$age", 18 }) }, { "then", "Under 18" } },
                                    new BsonDocument { { "case", new BsonDocument("$lte", new BsonArray { "$age", 25 }) }, { "then", "18-25" } },
                                    new BsonDocument { { "case", new BsonDocument("$lte", new BsonArray { "$age", 35 }) }, { "then", "26-35" } },
                                    new BsonDocument { { "case", new BsonDocument("$lte", new BsonArray { "$age", 50 }) }, { "then", "36-50" } },
                                    new BsonDocument { { "case", new BsonDocument("$gt", new BsonArray { "$age", 50 }) }, { "then", "Over 50" } }
                                }
                            },
                            { "default", "Unknown" }
                        })
                    }
                }),
                new BsonDocument("$group", new BsonDocument { { "_id", "$ageGroup" }, { "Value", new BsonDocument("$sum", 1) } }),
                new BsonDocument("$project", new BsonDocument { { "GroupKey", "$_id" }, { "Value", "$Value" }, { "_id", 0 } })
            };
        }
    }
}
