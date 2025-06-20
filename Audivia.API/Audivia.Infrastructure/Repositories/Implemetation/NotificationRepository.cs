using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using MongoDB.Driver;

namespace Audivia.Infrastructure.Repositories.Implemetation
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        private readonly IMongoCollection<UserFollow> _userFollowCollection;
        public NotificationRepository(IMongoDatabase database) : base(database)
        {
            _userFollowCollection = database.GetCollection<UserFollow>("UserFollow");
        }
        public async Task<List<Notification>> GetNotificationsByUserIdAsync(string userId)
        {
            // Lấy danh sách các FollowingId mà user đang follow
            var followingIds = await _userFollowCollection
                .Find(uf => uf.FollowerId == userId && uf.IsDeleted == false)
                .Project(uf => uf.FollowingId!)
                .ToListAsync();

            // Tạo filter riêng biệt
            var builder = Builders<Notification>.Filter;
            var selfFilter = builder.Eq(n => n.UserId, userId) & builder.Eq(n => n.IsDeleted, false) & builder.Ne(n => n.Type, "Bài viết");
            var followingFilter = builder.In(n => n.UserId, followingIds) &
                                  builder.Eq(n => n.Type, "Bài viết") &
                                  builder.Eq(n => n.IsDeleted, false);

            var combinedFilter = builder.Or(selfFilter, followingFilter);

            var notifications = await _collection
                .Find(combinedFilter)
                .SortByDescending(n => n.CreatedAt)
                .ToListAsync();

            return notifications;
        }

        public async Task<int> CountUnreadNotificationAsync(string userId)
        {
            var filter = Builders<Notification>.Filter.And(
            Builders<Notification>.Filter.Eq(n => n.UserId, userId),
            Builders<Notification>.Filter.Eq(n => n.IsRead, false),
            Builders<Notification>.Filter.Eq(n => n.IsDeleted, false)
            );

            return (int)await _collection.CountDocumentsAsync(filter);
        }

        public async Task<List<Notification>> FindByUserAndTourAsync(string userId, string tourId, string type)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.UserId, userId)
                       & Builders<Notification>.Filter.Eq(n => n.TourId, tourId)
                       & Builders<Notification>.Filter.Eq(n => n.Type, type)
                       & Builders<Notification>.Filter.Eq(n => n.IsDeleted, false);

            return await _collection.Find(filter).ToListAsync();
        }
    }
}
