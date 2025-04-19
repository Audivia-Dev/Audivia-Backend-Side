using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.DTOs
{
    public class UserCurrentLocationDTO
    {
        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public double? Accuracy { get; set; }
    }
}
