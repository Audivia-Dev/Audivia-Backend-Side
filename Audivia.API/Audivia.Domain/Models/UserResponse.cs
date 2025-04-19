using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Models
{
    public class UserResponse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }
        [BsonElement("question_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? QuestionId { get; set; }
        [BsonElement("answer_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? AnswerId { get; set; }
        [BsonElement("responded_at")]
        public DateTime? RespondedAt { get; set; }
    }
}
