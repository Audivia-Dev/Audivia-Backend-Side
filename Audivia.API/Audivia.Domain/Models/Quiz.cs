using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.Models
{
    public class Quiz
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("quiz_field_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? QuizFieldId { get; set; }

        [BsonElement("tour_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TourId { get; set; }

        [BsonElement("title")]
        public string? Title { get; set; }
        [BsonElement("image")]
        public string? Image { get; set; }

        [BsonElement("questions_count")]
        public int QuestionsCount { get; set; } = 0;
        
        // default fields
        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("is_deleted")]
        public bool? IsDeleted { get; set; }

        // navigation props
        [BsonIgnore]
        public QuizField? QuizFieldData { get; set; }
        [BsonIgnore]
        public Tour? TourData { get; set; }
    }
}
