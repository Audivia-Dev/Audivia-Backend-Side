namespace Audivia.Domain.ModelRequests.UserFollow
{
    public class UpdateUserFollowRequest
    {
        public string? FollowerId { get; set; }
        public string? FollowingId { get; set; }
    }
}
