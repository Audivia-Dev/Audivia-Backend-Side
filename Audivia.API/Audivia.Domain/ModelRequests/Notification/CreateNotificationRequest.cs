namespace Audivia.Domain.ModelRequests.Notification
{
    public class CreateNotificationRequest
    {
        public string? UserId { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; } // enum
        public bool IsRead { get; set; } = false;
        public string? TourId { get; set; } 
    }
}
