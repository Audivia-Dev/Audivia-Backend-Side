using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Notification;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Notification
{
    [Route("api/v1/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNotificationRequest request)
        {
            var result = await _notificationService.CreateNotification(request);
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
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _notificationService.DeleteNotification(id);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetNotificationsByUser(string userId)
        {
            var rs = await _notificationService.GetNotificationsByUserIdAsync(userId);
            return Ok(rs);
        }
    }
}
