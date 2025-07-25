using Audivia.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Models
{
    [BsonIgnoreExtraElements]
    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("quiz_id")]
        public string? QuizId { get; set; }

        [BsonElement("type")]
        public QuestionType Type { get; set; } // e.g., "MultipleChoice", "TrueFalse", etc.

        [BsonElement("text")]
        public string? Text { get; set; }

        [BsonElement("imageUrl")]
        public string? ImageUrl { get; set; }

        [BsonElement("points")]
        public double? Points { get; set; }

        [BsonElement("order")]
        public int Order { get; set; }
       
        // [BsonIgnore]
        public List<Answer>? Answers { get; set; } = new List<Answer>();

        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
