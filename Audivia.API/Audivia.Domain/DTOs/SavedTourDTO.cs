using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.DTOs
{
    public class SavedTourDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public string? TourId { get; set; }
        public DateTime? SavedAt { get; set; }
        public DateTime? PlannedTime { get; set; }
    }
}
