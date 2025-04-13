
namespace Audivia.Domain.ModelRequests.User
{
    public class LoginRequest 
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
