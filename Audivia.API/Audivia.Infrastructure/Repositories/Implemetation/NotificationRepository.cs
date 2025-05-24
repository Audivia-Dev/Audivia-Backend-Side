using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(IMongoDatabase database) : base(database)
        {

        }
        public async Task<List<Notification>> GetNotificationsByUserIdAsync(string userId)
        {
            return  await _collection.Find(n => n.UserId == userId).SortByDescending(n => n.CreatedAt).ToListAsync();
        }
    }
}
