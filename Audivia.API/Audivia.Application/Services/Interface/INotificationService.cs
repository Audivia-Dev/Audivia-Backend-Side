using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.Notification;
using Audivia.Domain.ModelResponses.Notification;
using Audivia.Domain.Models;

namespace Audivia.Application.Services.Interface
{
    public interface INotificationService
    {
        Task<NotificationResponse> CreateNotification(CreateNotificationRequest request);

        Task<NotificationListResponse> GetAllNotifications();

        Task<NotificationResponse> GetNotificationById(string id);

        Task UpdateNotification(string id, UpdateNotificationRequest request);

        Task DeleteNotification(string id);

        Task<NotificationListResponse> GetNotificationsByUserIdAsync(string userId);

        Task<int> CountUnreadNotificationAsync(string userId);
    }
}
