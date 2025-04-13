

using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.User
{
    public class UserResponse : AbstractApiResponse<UserDTO>
    {
        public override UserDTO Response { get; set; }
    }
}
