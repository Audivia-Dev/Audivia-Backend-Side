using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.DTOs
{
    public class GroupMemberDTO
    {
        public string Id { get; set; }
        public string? UserId { get; set; }
        public string? GroupId { get; set; }
        public DateTime? JoinedAt { get; set; }
    }
}
