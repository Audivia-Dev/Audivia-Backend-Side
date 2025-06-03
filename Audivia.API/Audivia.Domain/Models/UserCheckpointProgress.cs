using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.Models
{
    public class UserCheckpointProgress
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("user_tour_progress_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserTourProgressId { get; set; }

        [BsonElement("tour_checkpoint_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TourCheckpointId { get; set; }

        [BsonElement("checkpoint_audio_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CheckpointAudioId { get; set; }


        [BsonElement("progress_seconds")]
        public int ProgressSeconds { get; set; } = 0;

        [BsonElement("is_completed")]
        public bool IsCompleted { get; set; } = false;

        [BsonElement("last_listened_time")]
        public DateTime? LastListenedTime { get; set; }

    }
}
