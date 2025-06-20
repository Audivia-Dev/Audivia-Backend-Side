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
            var objectId = new ObjectId(userId);
            return await GetSavedTourWithTourLookup()
                .Match(t => t.UserId == userId).ToListAsync();
        }

        public async Task<SavedTour?> FindByUserIdAndTourIdAsync(string userId, string tourId)
        {
            return await _collection.Find(t => t.UserId == userId && t.TourId == tourId).FirstOrDefaultAsync();
        }
        public async Task<SavedTour?> GetByIdWithTour(string id)
        {
            var objectId = new ObjectId(id);
            return await GetSavedTourWithTourLookup()
                .Match(t => t.Id == id || t.Id == objectId.ToString())
                .FirstOrDefaultAsync();
        }
        private IAggregateFluent<SavedTour> GetSavedTourWithTourLookup()
        {
            return _collection.Aggregate()
                .Lookup(
                    foreignCollectionName: "Tour",
                    localField: "tour_id",
                    foreignField: "_id",
                    @as: "tour"
                )
                .Unwind("tour", new AggregateUnwindOptions<SavedTour>
                {
                    PreserveNullAndEmptyArrays = true
                });
        }

        public async Task<List<SavedTour>> GetUpcomingToursAsync(DateTime fromTime, DateTime toTime)
        {
            var filter = Builders<SavedTour>.Filter.And(
                Builders<SavedTour>.Filter.Gte(x => x.PlannedTime, fromTime),
                Builders<SavedTour>.Filter.Lte(x => x.PlannedTime, toTime)
                );
            return await _collection.Find(filter).ToListAsync();
        }



    }
}
