using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.Notification;
using Audivia.Domain.ModelResponses.Notification;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<NotificationResponse> CreateNotification(CreateNotificationRequest request)
        {
            if (!ObjectId.TryParse(request.UserId, out _))
            {
                return new NotificationResponse
                {
                    Success = false,
                    Message = "Invalid UserId format",
                    Response = null
                };
            }
            var notification = new Notification
            {
                UserId = request.UserId,
                Content = request.Content,
                Type = request.Type,
                IsRead = request.IsRead,
                CreatedAt = DateTime.UtcNow
            };

            await _notificationRepository.Create(notification);

            return new NotificationResponse
            {
                Success = true,
                Message = "Notification created successfully",
                Response = ModelMapper.MapNotificationToDTO(notification)
            };
        }

        public async Task<NotificationListResponse> GetAllNotifications()
        {
            var notifications = await _notificationRepository.GetAll();
            var notificationDtos = notifications
                .Where(t => t.IsDeleted == false)
                .Select(ModelMapper.MapNotificationToDTO)
                .ToList();
            return new NotificationListResponse
            {
                Success = true,
                Message = "Notifications retrieved successfully",
                Response = notificationDtos
            };
        }

        public async Task<NotificationResponse> GetNotificationById(string id)
        {
            var notification = await _notificationRepository.FindFirst(t => t.Id == id && t.IsDeleted == false);
            if (notification == null)
            {
                return new NotificationResponse
                {
                    Success = false,
                    Message = "Notification not found",
                    Response = null
                };
            }

            return new NotificationResponse
            {
                Success = true,
                Message = "Notification retrieved successfully",
                Response = ModelMapper.MapNotificationToDTO(notification)
            };
        }

        public async Task UpdateNotification(string id, UpdateNotificationRequest request)
        {
            var notification = await _notificationRepository.FindFirst(t => t.Id == id && t.IsDeleted == false);
            if (notification == null) return;

            
            notification.UserId = request.UserId ?? notification.UserId;
            notification.Content = request.Content ?? notification.Content;
            notification.Type = request.Type ?? notification.Type;

            await _notificationRepository.Update(notification);
        }

        public async Task DeleteNotification(string id)
        {
            var notification = await _notificationRepository.FindFirst(t => t.Id == id );
            if (notification == null) return;

            notification.IsDeleted = true;

            await _notificationRepository.Update(notification);
        }
    }
}
