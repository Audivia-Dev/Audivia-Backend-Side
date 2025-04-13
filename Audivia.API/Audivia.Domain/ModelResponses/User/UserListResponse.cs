
using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.User
{
    public class UserListResponse : AbstractApiResponse<List<UserDTO>>
    {
        public override List<UserDTO> Response { get; set; }
    }
}
