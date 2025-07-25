
using MongoDB.Bson.Serialization.Attributes;

namespace Audivia.Domain.Models
{
    public class UserQuestionResponse
    {
        [BsonElement("user_id")]
        public string UserId { get; set; }
        
        [BsonElement("quiz_id")]
        public string QuizId { get; set; }

        [BsonElement("question_id")]
        public string QuestionId { get; set; }

        [BsonElement("answer_id")]
        public string AnswerId { get; set; }

        [BsonElement("is_correct")]
        public bool IsCorrect {  get; set; }
    }
}
