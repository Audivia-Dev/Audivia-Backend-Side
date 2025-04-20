using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Models
{
    public class Group
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }
        [BsonElement("is_deleted")]
        public bool? IsDeleted { get; set; }
    }
}
