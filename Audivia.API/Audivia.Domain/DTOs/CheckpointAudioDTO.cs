namespace Audivia.Domain.DTOs
{
    public class CheckpointAudioDTO
    {
        public string Id { get; set; } = string.Empty;
        public string TourCheckpointId { get; set; }
        public string AudioCharacterId { get; set; }
        public string? FileUrl { get; set; }
        public bool IsDefault { get; set; } = true;
        public string? Transcript { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
