namespace Audivia.Domain.DTOs
{
    public class AnswerDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? QuestionId { get; set; }
        public string? Text { get; set; }
    }
}
