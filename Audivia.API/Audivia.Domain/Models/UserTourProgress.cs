using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Audivia.Domain.Models
{
    public class UserTourProgress
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("tour_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TourId { get; set; }

        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        [BsonElement("started_at")]
        public DateTime? StartedAt { get; set; }

        [BsonElement("finished_at")]
        public DateTime? FinishedAt { get; set; }

        [BsonElement("is_completed")]
        public bool IsCompleted { get; set; } = false;

        [BsonElement("current_checkpoint_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CurrentCheckpointId { get; set; }

        [BsonElement("score")]
        public int? Score { get; set; } = 0;

        [BsonElement("group_mode")]
        public bool GroupMode { get; set; } = false;

        [BsonElement("group_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? GroupId { get; set; }

    }
}
