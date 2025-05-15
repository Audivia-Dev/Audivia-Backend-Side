using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("type")]
        public string Type { get; set; } // enum

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        public string Status { get; set; } // enum

        [BsonElement("sender_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SenderId { get; set; }

        [BsonElement("chatroom_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ChatRoomId { get; set; }

        [BsonElement("Sender")]
        [BsonIgnoreIfNull]
        public User? Sender { get; set; }
    }
}
