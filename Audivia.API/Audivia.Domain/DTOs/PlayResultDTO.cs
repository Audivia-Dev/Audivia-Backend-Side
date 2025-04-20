using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.DTOs
{
    public class PlayResultDTO
    {
        public string Id { get; set; }
        public string? SessionId { get; set; }
        public double? Score { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
