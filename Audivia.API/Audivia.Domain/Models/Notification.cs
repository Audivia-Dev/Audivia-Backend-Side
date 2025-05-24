using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.Models
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        [BsonElement("content")]
        public string? Content { get; set; }

        [BsonElement("type")]
        public string? Type { get; set; } // enum

        [BsonElement("is_read")]
        public bool IsRead { get; set; }

        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }
        [BsonElement("is_deleted")]
        public bool IsDeleted {  get; set; }
        [BsonElement("tour_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TourId { get; set; }
    }
}
