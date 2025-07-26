namespace Audivia.Domain.DTOs
{
    public class UserQuestionResponseDTO
    {
        public string? UserId { get; set; }
        public string? QuizId { get; set; }
        public string? QuestionId { get; set; }
        public string? AnswerId { get; set; }
        public bool? IsCorrect { get; set; }
        public string? TrueAnswerNote { get; set; }
    }
}
