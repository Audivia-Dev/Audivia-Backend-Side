using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Audivia.Domain.Enums;

namespace Audivia.Domain.Models
{
    public class Reaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("type")]
        [BsonRepresentation(BsonType.String)]
        public ReactionType Type { get; set; }

        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("created_by")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CreatedBy { get; set; }

        [BsonElement("post_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? PostId { get; set; }
    }
}
