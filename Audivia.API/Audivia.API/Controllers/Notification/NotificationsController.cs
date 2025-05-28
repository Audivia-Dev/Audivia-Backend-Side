using Audivia.API.Hubs;
using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Audivia.API.Controllers.Notification
{
    [Route("api/v1/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationsController(INotificationService notificationService, IHubContext<NotificationHub> hubContext)
        {
            _notificationService = notificationService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNotificationRequest request)
        {
            var result = await _notificationService.CreateNotification(request);
            var countUnread = await _notificationService.CountUnreadNotificationAsync(request.UserId);

            //Send Client
            await _hubContext.Clients.Group(request.UserId).SendAsync("ReceiveUnreadCount", countUnread);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _notificationService.GetAllNotifications();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _notificationService.GetNotificationById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateNotificationRequest request)
        {
            await _notificationService.UpdateNotification(id, request);
            var countUnread = await _notificationService.CountUnreadNotificationAsync(request.UserId);

            //Send Client
            await _hubContext.Clients.Group(request.UserId).SendAsync("ReceiveUnreadCount", countUnread);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var notification = await _notificationService.GetNotificationById(id);
            if (notification == null)
                return NotFound();

            await _notificationService.DeleteNotification(id);
            await _hubContext.Clients.Group(notification.Response.UserId)
                .SendAsync("NotificationDeleted", id);
            var countUnread = await _notificationService.CountUnreadNotificationAsync(notification.Response.UserId);
            await _hubContext.Clients.Group(notification.Response.UserId).SendAsync("ReceiveUnreadCount", countUnread);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetNotificationsByUser(string userId)
        {
            var rs = await _notificationService.GetNotificationsByUserIdAsync(userId);
            return Ok(rs);
        }
        [HttpGet("unread-count/{userId}")]
        public async Task<IActionResult> CountUnreadNotification(string userId)
        {
            var rs = await _notificationService.CountUnreadNotificationAsync(userId);
            return Ok(rs);
        }
    }
}
