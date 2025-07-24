namespace Audivia.Domain.DTOs
{
    public class UserQuizResponseDTO
    {
        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public string? QuizId { get; set; }
        public int CorrectAnswersCount { get; set; }
        public bool IsDone { get; set; }
        public DateTime? RespondedAt { get; set; }
        public List<UserQuestionResponseDTO> QuestionAnswers { get; set; }
    }
}
