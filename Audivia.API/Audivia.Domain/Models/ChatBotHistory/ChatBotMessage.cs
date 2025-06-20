using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace Audivia.Domain.Models.ChatBotHistory
{
    public enum SenderType
    {
        User,
        Bot
    }

    public class ChatBotMessage
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ChatSessionId { get; set; } 
        public string ClientSessionId { get; set; }
        public SenderType Sender { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual ChatBotSession Session { get; set; }
    }
} 