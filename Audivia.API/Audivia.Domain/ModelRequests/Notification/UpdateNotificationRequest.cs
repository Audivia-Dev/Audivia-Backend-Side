namespace Audivia.Domain.ModelRequests.Notification
{
    public class UpdateNotificationRequest
    {
        public string? UserId { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
