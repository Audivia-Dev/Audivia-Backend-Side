using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;
using MongoDB.Bson;
using Audivia.Domain.ModelResponses.Statistics;
using Audivia.Domain.ModelRequests.Statistics;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IMongoDatabase database) : base(database) { }

        public async Task<User?> GetByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByTokenConfirm(string token)
        {
            var filter = Builders<User>.Filter.Eq(u => u.TokenConfirmEmail, token);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByUsername(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<UserStatItem>> GetUserStatisticsAsync(GetUserStatRequest request)
        {
            var pipeline = new List<BsonDocument>();
            var matchFilter = new BsonDocument("is_deleted", false);

            if (request.StatType == "new_users")
            {
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
            }
            pipeline.Add(new BsonDocument("$match", matchFilter));

            var groupStage = BuildUserGroupStage(request.GroupBy);
            if(groupStage != null)
            {
                pipeline.AddRange(groupStage);
            }
            else
            {
                // If no grouping, just count the matched documents
                 pipeline.Add(new BsonDocument("$group", new BsonDocument { { "_id", BsonNull.Value }, { "Count", new BsonDocument("$sum", 1) } }));
                 pipeline.Add(new BsonDocument("$project", new BsonDocument { { "_id", 0 }, { "GroupKey", "Total" }, { "Count", "$Count" }}));
            }
            
            pipeline.Add(new BsonDocument("$sort", new BsonDocument("GroupKey", 1)));

            return await _collection.Aggregate<UserStatItem>(pipeline).ToListAsync();
        }
        
        private IEnumerable<BsonDocument> BuildUserGroupStage(string groupBy)
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
                            { "Count", new BsonDocument("$sum", 1) }
                        }),
                        new BsonDocument("$project", new BsonDocument { { "GroupKey", "$_id" }, { "Count", "$Count" }, { "_id", 0 } })
                    };
                case "user_age_group":
                    return BuildUserAgeGroupPipeline();
                default:
                   return null;
            }
        }
        
        private IEnumerable<BsonDocument> BuildUserAgeGroupPipeline()
        {
            return new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument("birth_day", new BsonDocument("$ne", BsonNull.Value))),
                new BsonDocument("$addFields", new BsonDocument
                {
                    { "age", new BsonDocument("$dateDiff", new BsonDocument { { "startDate", "$birth_day" }, { "endDate", "$$NOW" }, { "unit", "year" } }) }
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
                new BsonDocument("$group", new BsonDocument { { "_id", "$ageGroup" }, { "Count", new BsonDocument("$sum", 1) } }),
                new BsonDocument("$project", new BsonDocument { { "GroupKey", "$_id" }, { "Count", "$Count" }, { "_id", 0 } })
            };
        }
    }
}
