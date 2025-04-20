using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Models
{
    public class QuizField
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("quiz_field_name")]
        public string? QuizFieldName { get; set; }
        [BsonElement("description")]
        public string? Description { get; set; }
        [BsonElement("is_deleted")]
        public bool? IsDeleted { get; set; } = false;   

    }
}
