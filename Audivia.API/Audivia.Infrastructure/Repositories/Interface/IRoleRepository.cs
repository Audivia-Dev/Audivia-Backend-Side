using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface IRoleRepository : IBaseRepository<Role>, IDisposable
    {
        Task<Role?> GetByRoleName(string roleName);
    }
}
