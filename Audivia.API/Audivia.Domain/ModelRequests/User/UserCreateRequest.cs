using System.ComponentModel.DataAnnotations;

namespace Audivia.Domain.ModelRequests.User

{
    public class UserCreateRequest
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string RoleName { get; set; } = string.Empty;

        public DateOnly? BirthDay { get; set; }
    }
}
