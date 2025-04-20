using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.Models
{
    public class CheckpointAudio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }

        [BsonElement("checkpoint_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CheckpointId { get; set; }

        [BsonElement("audio_character_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AudioCharacterId { get; set; }

        [BsonElement("file_url")]
        public string? FileUrl { get; set; }

        [BsonElement("is_default")]
        public bool IsDefault { get; set; } = true;

        [BsonElement("transcript")]
        public string? Transcript { get; set; }

        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}