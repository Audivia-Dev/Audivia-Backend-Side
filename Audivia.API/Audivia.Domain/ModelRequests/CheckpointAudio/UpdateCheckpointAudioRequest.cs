namespace Audivia.Domain.ModelRequests.CheckpointAudio
{
    public class UpdateCheckpointAudioRequest
    {
        public string? CheckpointId { get; set; }
        public string? AudioCharacterId { get; set; }
        public string? FileUrl { get; set; }
        public string? VideoUrl { get; set; }
        public bool? IsDefault { get; set; } // default audio for the tour
        public string? Transcript { get; set; }
    }
}
