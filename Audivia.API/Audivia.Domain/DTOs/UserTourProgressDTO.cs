using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.DTOs
{
    public class UserTourProgressDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? TourId { get; set; }
        public string? UserId { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public string? Status { get; set; }
        public string? CurrentCheckpointId { get; set; }
        public int? Score { get; set; }
        public bool GroupMode { get; set; }
        public string? GroupId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
