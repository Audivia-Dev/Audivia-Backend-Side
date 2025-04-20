using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.DTOs
{
    public class VoucherDTO
    {
        public string Id { get; set; } = string.Empty;

        public string? Code { get; set; }
        public float? Discount { get; set; }
        public string? Title { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
