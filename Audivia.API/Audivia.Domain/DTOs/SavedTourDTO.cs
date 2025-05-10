using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Audivia.Domain.Models;

namespace Audivia.Domain.DTOs
{
    public class SavedTourDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public string? TourId { get; set; }
        public DateTime? SavedAt { get; set; }
        public DateTime? PlannedTime { get; set; }
        public Tour? Tour { get; set; }
        public string? TimeAgo { get; set; }
    }
}
