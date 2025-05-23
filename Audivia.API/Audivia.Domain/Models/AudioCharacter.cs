﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Audivia.Domain.Models
{
    public class AudioCharacter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("voice_type")]
        public string? VoiceType { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("avatar_url")]
        public string? AvatarUrl { get; set; }
        [BsonElement("audio_url")]
        public string? AudioUrl { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
