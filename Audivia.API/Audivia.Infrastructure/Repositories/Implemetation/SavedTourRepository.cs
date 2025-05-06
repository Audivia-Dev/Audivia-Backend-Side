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
            return await _collection.Find(t => t.UserId == userId).ToListAsync();
        }
    }
}
