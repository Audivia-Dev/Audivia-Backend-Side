using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.Models
{
    public class CheckpointImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("tourcheckpoint_id")]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string? TourCheckpointId { get; set; }

        [BsonElement("image_url")]
        public string? ImageUrl { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}