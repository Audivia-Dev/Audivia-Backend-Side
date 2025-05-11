namespace Audivia.Domain.ModelRequests.UserFollow
{
    public class GetFollowRequest
    {
        public string CurrentUserId { get; set; }
        public string TargetUserId {  get; set; }
    }
}
