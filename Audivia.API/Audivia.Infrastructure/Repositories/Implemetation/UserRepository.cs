using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

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
    }

}
