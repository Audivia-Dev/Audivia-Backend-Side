using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class UserCheckpointProgressRepository : BaseRepository<UserCheckpointProgress>, IUserCheckpointProgressRepository
    {
        public UserCheckpointProgressRepository(IMongoDatabase database) : base(database)
        {
        }

        public async Task<List<UserCheckpointProgress>> GetByTourProgressId(string tourProgressId)
        {
            return await _collection.Find(c => c.UserTourProgressId == tourProgressId).ToListAsync();
        }
    }
}
