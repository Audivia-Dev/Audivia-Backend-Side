﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Audivia.Domain.Models;

namespace Audivia.Domain.DTOs
{
    public class ChatRoomDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public string Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<ChatRoomMember> Members { get; set; }
    }
}
