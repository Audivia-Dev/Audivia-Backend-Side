namespace Audivia.Domain.DTOs
{
    public class CommentDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? PostId { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
