using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.DTOs
{
    public class NotificationDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; } // enum
        public bool IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
