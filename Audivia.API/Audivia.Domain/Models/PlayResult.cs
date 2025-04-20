using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Models
{
    public class PlayResult
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("session_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? SessionId { get; set; }

        [BsonElement("score")]
        public double? Score { get; set; }
        [BsonElement("CompletedAt")]
        public DateTime? CompletedAt { get; set; }
    }
}
