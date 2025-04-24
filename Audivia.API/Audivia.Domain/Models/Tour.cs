using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.Models
{
    public class Tour
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("title")]
        public string? Title { get; set; }

        [BsonElement("location")]
        public string? Location { get; set; } 

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("price")]
        public decimal? Price { get; set; }

        [BsonElement("duration")]
        public int? Duration { get; set; }

        [BsonElement("type_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TypeId { get; set; }

        [BsonElement("tour_type")]
        public TourType? TourType { get; set; }

        [BsonElement("thumbnail_url")]
        public string? ThumbnailUrl { get; set; }

        [BsonElement("avg_rating")]
        public double AvgRating { get; set; } = 0;

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}