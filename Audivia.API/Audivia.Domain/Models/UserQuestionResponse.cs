
namespace Audivia.Domain.Models
{
    public class UserQuestionResponse
    {
        public string UserId { get; set; }
        public string QuizId { get; set; }
        public string QuestionId { get; set; }
        public string AnswerId { get; set; }
        public bool IsCorrect {  get; set; }
    }
}
