using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.DTOs
{
    public class UserFollowDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? FollowerId { get; set; }
        public string? FollowingId { get; set; }

        public bool AreFriends { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
