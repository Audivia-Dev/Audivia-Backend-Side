using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Models
{
    public class UserLocationVisit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }
        [BsonElement("tourcheckpoint_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TourcheckpointId { get; set; }
        [BsonElement("route_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RouteId { get; set; }
        [BsonElement("visited_at")]
        public DateTime? VisitedAt { get; set; }
        [BsonElement("is_deleted")]
        public bool? IsDeleted { get; set; }

    }
}
