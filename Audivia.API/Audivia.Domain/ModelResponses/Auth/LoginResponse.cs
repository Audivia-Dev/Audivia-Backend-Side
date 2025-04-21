using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.Auth
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public string AccessToken { get; set; } = "";

        public string RefreshToken { get; set; } = "";
    }
}
