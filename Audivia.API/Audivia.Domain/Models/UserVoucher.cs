using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Models
{
    public class UserVoucher
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }
        [BsonElement("voucher_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? VoucherId { get; set; }
        [BsonElement("used_at")]
        public DateTime? UsedAt { get; set; }
        [BsonElement("is_deleted")]
        public bool? IsDeleted { get; set; }

    }
}
