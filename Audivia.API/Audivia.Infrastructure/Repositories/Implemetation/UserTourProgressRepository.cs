using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class UserTourProgressRepository : BaseRepository<UserTourProgress>, IUserTourProgressRepository
    {
        public UserTourProgressRepository(IMongoDatabase database) : base(database) { }

        public async Task<UserTourProgress> GetByUserIdAndTourId(string userId, string tourId)
        {
            return await _collection.Find(c => c.TourId == tourId && c.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
