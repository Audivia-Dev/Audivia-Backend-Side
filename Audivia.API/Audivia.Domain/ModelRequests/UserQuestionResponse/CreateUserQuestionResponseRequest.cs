namespace Audivia.Domain.ModelRequests.UserQuestionResponse
{
    public class CreateUserQuestionResponseRequest
    {
        public string UserId { get; set; }
        public string QuizId { get; set; }
        public string QuestionId { get; set; }
        public string AnswerId { get; set; }
    }
}
