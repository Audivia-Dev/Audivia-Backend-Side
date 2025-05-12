
using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.User
{
    public class UserShortListResponse : AbstractApiResponse<List<UserShortDTO>>
    {
        public override List<UserShortDTO> Response { get; set; }
    }
}
