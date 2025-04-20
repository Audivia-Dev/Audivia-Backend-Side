using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.Models
{
    public class ChatRoomMember
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("chatroom_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ChatRoomId { get; set; }

        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("nickname")]
        public string Nickname { get; set; }

        [BsonElement("is_host")]
        public bool IsHost { get; set; }
        [BsonElement("joined_at")]
        public DateTime? JoinedAt { get; set; }
    }
}
