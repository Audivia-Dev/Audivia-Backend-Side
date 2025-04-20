using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Models
{
    public class Leaderboard
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("tour_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TourId { get; set; }

        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        [BsonElement("rank")]
        public string? Rank { get; set; }

        [BsonElement("score")]
        public double? Score { get; set; }
        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
