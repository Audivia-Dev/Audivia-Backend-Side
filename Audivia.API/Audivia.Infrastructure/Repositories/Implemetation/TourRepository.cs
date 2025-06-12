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
    }
}
