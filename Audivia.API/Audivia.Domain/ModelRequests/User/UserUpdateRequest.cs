namespace Audivia.Domain.ModelRequests.User
{
    public class UserUpdateRequest
    {
        public string? Phone { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public string? AudioCharacterId { get; set; }
        public int? AutoPlayDistance { get; set; }
        public int? TravelDistance { get; set; }
        public string? RoleId { get; set; }
    }
}
