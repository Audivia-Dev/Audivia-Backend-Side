namespace Audivia.Domain.DTOs
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Bio {  get; set; }
        public int BalanceWallet { get; set; }
        public string? AudioCharacterId { get; set; }
        public int? AutoPlayDistance { get; set; }
        public int? TravelDistance { get; set; }
        public string RoleId { get; set; }
        public bool ConfirmedEmail { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
