using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.DTOs
{
    public class UserCheckpointProgressDTO
    {
        public string Id { get; set; } = null!;
        public string UserTourProgressId { get; set; }

        public string TourCheckpointId { get; set; }

        public string CheckpointAudioId { get; set; }

        public int ProgressSeconds { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? LastListenedTime { get; set; }
    }
}
