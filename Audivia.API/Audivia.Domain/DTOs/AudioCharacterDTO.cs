namespace Audivia.Domain.DTOs
{
    public class AudioCharacterDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? VoiceType { get; set; }
        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
        public string? AudioUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
