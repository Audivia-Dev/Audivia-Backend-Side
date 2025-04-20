using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.DTOs
{
    public class UserLocationVisitDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public string? TourcheckpointId { get; set; }
        public DateTime? VisitedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
