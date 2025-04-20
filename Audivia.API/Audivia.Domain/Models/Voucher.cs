using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Models
{
    public class Voucher
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("code")]
        public string? Code { get; set; }
        [BsonElement("discount")]
        public float? Discount { get; set; }
        [BsonElement("title")]
        public string? Title { get; set; }
        [BsonElement("created_at")]
        public DateTime? CreatedAt { get; set; }
        [BsonElement("expiry_date")]
        public DateTime? ExpiryDate { get; set; }
        [BsonElement("is_deleted")]
        public bool? IsDeleted { get; set; }

    }
}
