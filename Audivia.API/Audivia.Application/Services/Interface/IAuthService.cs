using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.Auth;
using Audivia.Domain.ModelResponses.Auth;
using System.Security.Claims;

namespace Audivia.Application.Services.Interface
{
    public interface IAuthService
    {
        // login
        Task<LoginResponse> LoginWithEmailAndPassword(LoginRequest request);

        // register
        Task<RegisterResponse> Register(RegisterRequest request);

        //confirm email
        Task<string> VerifyEmail(ConfirmEmailRequest request);

        // get profile
        Task<UserDTO?> GetCurrentUserAsync(ClaimsPrincipal userClaims);

        Task<UserDTO?> GetCurrentUserAsync();
    }
}
