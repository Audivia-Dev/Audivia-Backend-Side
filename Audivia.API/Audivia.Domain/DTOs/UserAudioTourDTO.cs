namespace Audivia.Domain.DTOs
{
    public class UserAudioTourDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public string? TourId { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
