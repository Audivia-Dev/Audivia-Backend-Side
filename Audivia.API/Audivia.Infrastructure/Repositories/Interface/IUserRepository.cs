using Audivia.Infrastructure.Interface;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface IUserRepository: IBaseRepository<User>, IDisposable
    {
        Task<User?> GetByEmail(string email);
    }
}
