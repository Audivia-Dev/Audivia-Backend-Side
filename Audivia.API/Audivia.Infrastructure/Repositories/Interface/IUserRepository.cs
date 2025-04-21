using Audivia.Infrastructure.Interface;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface IUserRepository: IBaseRepository<User>, IDisposable
    {
        Task<User?> GetByEmail(string email);
        Task<User?> GetByUsername(string username);

        Task<User?> GetByTokenConfirm(string token);
    }
}
