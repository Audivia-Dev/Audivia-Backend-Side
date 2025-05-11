namespace Audivia.Domain.ModelRequests.UserFollow
{
    public class CreateUserFollowRequest
    {
        public string? FollowerId { get; set; }
        public string? FollowingId { get; set; }
    }
}
