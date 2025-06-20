using Audivia.Application.Services.Interface;
using Audivia.Application.Utils.Helper;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.Notification;
using Audivia.Domain.ModelResponses.Notification;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using Microsoft.AspNetCore.SignalR;
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
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                TourId = request.TourId,
                
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
            notification.IsRead = true;
            await _notificationRepository.Update(notification);
        }

        public async Task DeleteNotification(string id)
        {
            var notification = await _notificationRepository.FindFirst(t => t.Id == id );
            if (notification == null) return;

            notification.IsDeleted = true;

            await _notificationRepository.Update(notification);
        }

        public async Task<NotificationListResponse> GetNotificationsByUserIdAsync(string userId)
        {
            var notifications = await _notificationRepository.GetNotificationsByUserIdAsync(userId);
            if (notifications is null)
            {
                return new NotificationListResponse
                {
                    Success = false,
                    Message = "List not found",
                    Response = null
                };
            }
            var notificationDtos = notifications
                .Where(t => t.IsDeleted == false)
                .Select(n =>
                {
                    var dto = ModelMapper.MapNotificationToDTO(n);
                    dto.TimeAgo = TimeUtils.GetTimeElapsed((DateTime)n.CreatedAt);
                    return dto;
                }).ToList();
            return new NotificationListResponse
            {
                Success = true,
                Message = "Fetch list of notifiations successfully!",
                Response = notificationDtos
            };
        }
        public async Task<int> CountUnreadNotificationAsync(string userId)
        {
            return await _notificationRepository.CountUnreadNotificationAsync(userId);
        }
    }
}
