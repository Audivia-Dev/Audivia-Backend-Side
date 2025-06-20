using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface IUserTourProgressRepository : IBaseRepository<UserTourProgress>, IDisposable
    {
        Task<UserTourProgress> GetByUserIdAndTourId (string userId, string tourId);
    }
}
