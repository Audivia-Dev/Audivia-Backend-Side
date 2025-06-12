using Audivia.Domain.ModelResponses.TransactionHistory;
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



        //  return await _collection.Find(x => x.UserId == userId).ToListAsync();

        public async Task<TransactionHistory> GetTransactionHistoryByUserIdAndTourId(string userId, string tourId)
        {
            return await _collection.Find(x => x.UserId == userId && x.TourId == tourId).FirstOrDefaultAsync();
        }
    }
}

