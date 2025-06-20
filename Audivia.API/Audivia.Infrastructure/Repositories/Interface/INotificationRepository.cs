using Audivia.Domain.Models;
using Audivia.Infrastructure.Interface;

namespace Audivia.Infrastructure.Repositories.Interface
{
    public interface INotificationRepository : IBaseRepository<Notification>, IDisposable
    {
        Task<List<Notification>> GetNotificationsByUserIdAsync(string userId);

        Task<int> CountUnreadNotificationAsync(string userId);
        Task<List<Notification>> FindByUserAndTourAsync(string userId, string tourId, string type);

    }
}
