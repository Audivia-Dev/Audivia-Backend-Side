using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.DTOs
{
    public class MessageDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Content { get; set; }
        public string Type { get; set; } // enum
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } // enum
        public string SenderId { get; set; }
        public string ChatRoomId { get; set; }
    }
}
