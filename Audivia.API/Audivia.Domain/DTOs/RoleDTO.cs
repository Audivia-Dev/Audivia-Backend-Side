using MongoDB.Bson.Serialization.Attributes;

namespace Audivia.Domain.DTOs
{
    public class RoleDTO
    {
        public string Id { get; set; } = string.Empty;

        public string? RoleName { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
