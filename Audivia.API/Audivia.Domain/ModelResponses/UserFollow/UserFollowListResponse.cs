using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
namespace Audivia.Domain.ModelResponses.UserFollow
{
    public class UserFollowListResponse : AbstractApiResponse<List<UserFollowDTO>>
    {
        public override List<UserFollowDTO> Response { get; set; }
    }
}
