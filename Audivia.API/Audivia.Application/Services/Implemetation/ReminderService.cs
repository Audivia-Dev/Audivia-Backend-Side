using Audivia.Application.Services.Interface;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Implemetation
{
    public class ReminderService : IReminderService
    {
        private readonly ISavedTourRepository _savedTourRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly TimeSpan _notifyBefore = TimeSpan.FromHours(24);
        public ReminderService(ISavedTourRepository savedTourRepository, INotificationRepository notificationRepository)
        {
            _savedTourRepository = savedTourRepository;
            _notificationRepository = notificationRepository;
        }
        public async Task ProcessTourRemindersAsync()
        {
            var now = DateTime.UtcNow;
            var notifyTime = now.Add(_notifyBefore);
            
            var upcomingTours = await _savedTourRepository.GetUpcomingToursAsync(now, notifyTime);
            foreach (var tour in upcomingTours)
            {
                if (tour.UserId == null || tour.Tour == null) continue;
                var existingNotifications = await _notificationRepository.FindByUserAndTourAsync(tour.UserId, tour.TourId!, "Nhắc nhở");

                if (existingNotifications.Any()) continue;

                var notification = new Notification
                {
                    UserId = tour.UserId,
                    TourId = tour.TourId,
                    Content = $"Bạn có tour đã lên lịch vào {tour.PlannedTime:HH:mm dd/MM/yyyy}.",
                    Type = "Nhắc nhở",
                    IsRead = false,
                    IsDeleted = false,
                    CreatedAt = now,
                };
                await _notificationRepository.Create(notification);
            }

        }
    }
}
