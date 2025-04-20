namespace Audivia.Domain.DTOs
{
    public class AnswerDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? QuestionId { get; set; }
        public string? Text { get; set; }
        public bool? IsCorrect { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
