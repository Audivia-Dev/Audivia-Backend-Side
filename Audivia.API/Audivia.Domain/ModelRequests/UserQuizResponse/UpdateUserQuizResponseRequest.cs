namespace Audivia.Domain.ModelRequests.UserQuizResponse
{
    public class UpdateUserQuizResponseRequest
    {
        public string? UserId { get; set; }
        public string? QuizId { get; set; }
        public int? CorrectAnswersCount { get; set; }
        public bool? IsDone { get; set; }
        public DateTime? RespondedAt { get; set; }
    }
}
