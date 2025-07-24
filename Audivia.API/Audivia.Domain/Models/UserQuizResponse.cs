using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.Models
{
    public class UserQuizResponse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }
        
        [BsonElement("quiz_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? QuizId { get; set; }

        [BsonElement("correct_answers_count")]
        public int CorrectAnswersCount { get; set; }

        [BsonElement("question_answers")]
        public List<UserQuestionResponse>? QuestionAnswers { get; set; } = new List<UserQuestionResponse>();

        // check if user have done the quiz of that tour => no other quíz for that tour available for that user
        [BsonElement("is_done")]
        public bool IsDone { get; set; } = false; // done <=> QuestionAnswers count == total questions of quiz
        public DateTime? RespondedAt { get; set; }

    }
}
