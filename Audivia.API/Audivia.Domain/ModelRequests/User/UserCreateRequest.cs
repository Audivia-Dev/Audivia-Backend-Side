
namespace Audivia.Domain.ModelRequests.User

{
    public class UserCreateRequest
    {
        public required string UserName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public string RoleName { get; set; } = "admin";

    }
}
