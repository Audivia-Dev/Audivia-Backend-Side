

namespace Audivia.Domain.DTOs
{
    public class TourDTO
    {
        public string Id { get; set; } = string.Empty;

        public string? Title { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? Duration { get; set; }

        public string? TypeId { get; set; }

        public string? ThumbnailUrl { get; set; }
        public double AvgRating { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
