namespace Audivia.Domain.ModelRequests.UserQuestionResponse
{
    public class UpdateUserQuestionResponseRequest
    {
        public string? UserId { get; set; }
        public string? QuestionId { get; set; }
        public string? AnswerId { get; set; }
    }
}
