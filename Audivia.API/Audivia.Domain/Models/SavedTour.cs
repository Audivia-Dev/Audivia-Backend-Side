using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.Models
{
    public class SavedTour
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        [BsonElement("tour_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TourId { get; set; }

        [BsonElement("saved_at")]
        public DateTime? SavedAt { get; set; }

        [BsonElement("planned_time")] 
        public DateTime? PlannedTime { get; set; }

        [BsonElement("tour")]
        [BsonIgnoreIfNull]
        public Tour? Tour { get; set; }
    }
}
