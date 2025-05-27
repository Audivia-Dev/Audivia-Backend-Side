using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace Audivia.Domain.Models.ChatBotHistory
{
    public class ChatBotSession
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ClientSessionId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastAccessedAt { get; set; }
        public bool IsActive { get; set; }
    }
} 