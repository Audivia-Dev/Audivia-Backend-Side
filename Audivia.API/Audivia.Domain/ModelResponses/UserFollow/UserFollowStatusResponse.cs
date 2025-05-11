
namespace Audivia.Domain.ModelResponses.UserFollow
{
    public class UserFollowStatusResponse
    {
        public bool Success { get; set; } = false;
        public int FollowStatusNumber { get; set; }
        public string FollowStatusString { get; set; }
    }
}
