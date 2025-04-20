using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Models
{
    public class PlaySession
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }
        [BsonElement("route_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RouteId { get; set; }
        [BsonElement("group_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? GroupId { get; set; }
        [BsonElement("StartTime")]
        public DateTime? StartTime { get; set; }
        [BsonElement("EndTime")]
        public DateTime? EndTime { get; set; }
    }
}
