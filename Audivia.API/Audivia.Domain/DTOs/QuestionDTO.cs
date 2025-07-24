using Audivia.Domain.Enums;

namespace Audivia.Domain.DTOs
{
    public class QuestionDTO
    {
        public string Id { get; set; }
        public string? QuizId { get; set; }
        public QuestionType Type { get; set; } // e.g., "MultipleChoice", "TrueFalse", etc.
        public string? Text { get; set; }
        public string? ImageUrl { get; set; }
        public double? Points { get; set; }
        public List<AnswerDTO>? Answers { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
