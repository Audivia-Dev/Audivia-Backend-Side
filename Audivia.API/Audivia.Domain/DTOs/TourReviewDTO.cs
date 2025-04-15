namespace Audivia.Domain.DTOs
{
    public class TourReviewDTO
    {
        public string Id { get; set; } = string.Empty;

        public string? Title { get; set; }

        public string? ImageUrl { get; set; }

        public string? Content { get; set; }

        public int? Rating { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? TourId { get; set; }

        public string? CreatedBy { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
