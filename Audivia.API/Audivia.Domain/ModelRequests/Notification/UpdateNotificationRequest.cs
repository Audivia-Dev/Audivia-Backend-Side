namespace Audivia.Domain.ModelRequests.Notification
{
    public class UpdateNotificationRequest
    {
        public string? UserId { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
