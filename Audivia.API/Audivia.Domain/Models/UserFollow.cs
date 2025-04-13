using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Audivia.Domain.Models
{
    public class UserFollow
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("follower_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? FollowerId { get; set; }

        [BsonElement("following_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? FollowingId { get; set; }

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
