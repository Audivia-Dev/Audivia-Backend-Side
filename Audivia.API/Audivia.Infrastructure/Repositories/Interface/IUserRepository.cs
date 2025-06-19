using Audivia.Infrastructure.Interface;
using Audivia.Domain.ModelRequests.Statistics;
using Audivia.Domain.ModelResponses.Statistics;
using Audivia.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface IUserRepository: IBaseRepository<User>, IDisposable
    {
        Task<User?> GetByEmail(string email);
        Task<User?> GetByUsername(string username);

        Task<User?> GetByTokenConfirm(string token);

        Task<List<UserStatItem>> GetUserStatisticsAsync(GetUserStatRequest request);
    }
}
