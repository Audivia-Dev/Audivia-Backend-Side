using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Models
{
    public class Route
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }
        [BsonElement("IsDeleted")]
        public bool? IsDeleted { get; set; }
    }
}
