using Audivia.Domain.Models;

namespace Audivia.Domain.DTOs
{
    public class QuizDTO
    {
        public string Id { get; set; } = null!;

        public string? QuizFieldId { get; set; }
        public QuizField? QuizField { get; set; }

        public string? TourId { get; set; }
        public Tour? Tour { get; set; }
        public int QuestionsCount { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
