using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Models
{
    public class Answer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("question_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? QuestionId { get; set; }
        [BsonElement("text")]
        public string? Text { get; set; }

        [BsonElement("is_correct")]
        public bool? IsCorrect { get; set; }
        [BsonElement("is_deleted")]
        public bool? IsDeleted { get; set; }
    }
}
