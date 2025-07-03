namespace Audivia.Domain.ModelRequests.User
{
    public class UserUpdateRequest
    {
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CoverPhoto { get; set; }
        public string? Bio { get; set; }
        public double? BalanceWallet { get; set; }
        public string? AudioCharacterId { get; set; }
        public int? AutoPlayDistance { get; set; }
        public int? TravelDistance { get; set; }
        // ask at page "Let us know you better"
        public DateOnly? BirthDay { get; set; }
        public bool? Gender { get; set; }
        public string? Country { get; set; }
        public string? Job { get; set; }
    }
}
