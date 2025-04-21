using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IMongoDatabase database) : base(database)
        {
        }

        public async Task<Role?> GetByRoleName(string roleName)
        {
            var filter = Builders<Role>.Filter.Eq(r => r.RoleName, roleName);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

    }
}
