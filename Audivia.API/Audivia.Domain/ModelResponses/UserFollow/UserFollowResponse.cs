using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.UserFollow
{
    public class UserFollowResponse : AbstractApiResponse<UserFollowDTO>
    {
        public override UserFollowDTO Response { get; set; }
    }
}
