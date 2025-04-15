namespace Audivia.Domain.DTOs
{
    public class ReactionDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? PostId { get; set; }
        public string? CreatedBy { get; set; }
    }
}
