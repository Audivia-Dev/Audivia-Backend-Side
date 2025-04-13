
namespace Audivia.Domain.ModelRequests.User

{
    public class UserCreateRequest
    {
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
