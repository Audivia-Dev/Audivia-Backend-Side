namespace Audivia.Domain.DTOs
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
