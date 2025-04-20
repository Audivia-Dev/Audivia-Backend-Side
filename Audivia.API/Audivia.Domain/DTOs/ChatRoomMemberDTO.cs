using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.DTOs
{
    public class ChatRoomMemberDTO
    {
        public string Id { get; set; } = string.Empty;
        public string ChatRoomId { get; set; }
        public string UserId { get; set; }
        public string Nickname { get; set; }
        public bool IsHost { get; set; }
        public DateTime? JoinedAt { get; set; }
    }
}
