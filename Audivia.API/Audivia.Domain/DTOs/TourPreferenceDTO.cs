namespace Audivia.Domain.DTOs
{
    public class TourPreferenceDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public string? TourId { get; set; }
        public int? PredictedScore { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
