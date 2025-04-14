using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Models
{
    public class Quiz
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("quiz_field_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? QuizField { get; set; }

        [BsonElement("tourcheckpoint_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TourCheckpointId { get; set; }

        [BsonElement("title")]
        public string? Title { get; set; }
        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("is_deleted")]
        public bool? IsDeleted { get; set; }
        [BsonIgnore]
        public QuizField? QuizFieldData { get; set; }
        [BsonIgnore]
        public TourCheckpoint? TourCheckpointData { get; set; }
    }
}
