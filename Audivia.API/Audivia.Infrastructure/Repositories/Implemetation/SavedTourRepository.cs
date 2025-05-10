using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class SavedTourRepository : BaseRepository<SavedTour>, ISavedTourRepository
    {
        public SavedTourRepository(IMongoDatabase database) : base(database)
        {

        }
        public async Task<List<SavedTour>> GetSavedTourByUserId(string userId)
        {
          //  return await _collection.Find(t => t.UserId == userId).ToListAsync();
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument("user_id", new ObjectId(userId))),
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "Tour" },
                    { "localField", "tour_id" },
                    { "foreignField", "_id" },
                    { "as", "tour" }
                }),
                new BsonDocument("$unwind", new BsonDocument
                {
                    { "path", "$tour" },
                    { "preserveNullAndEmptyArrays", true }
                })
            };

            var aggregate = await _collection.Aggregate<SavedTour>(pipeline).ToListAsync();
            return aggregate;
        }
    }
}
