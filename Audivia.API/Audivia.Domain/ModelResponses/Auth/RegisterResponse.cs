using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.Auth
{
    public class RegisterResponse : AbstractApiResponse<UserDTO>
    {
        public override UserDTO Response { get; set; }
    }
}
