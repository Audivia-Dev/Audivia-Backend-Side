using Audivia.Domain.ModelRequests.Auth;
using Audivia.Domain.ModelResponses.Auth;

namespace Audivia.Application.Services.Interface
{
    public interface IAuthService
    {
        // login
        Task<LoginResponse> LoginWithEmailAndPassword(LoginRequest request);

        // register
        Task<RegisterResponse> Register(RegisterRequest request);

        //confirm email
        Task<ConfirmEmailResponse> VerifyEmail(ConfirmEmailRequest request);

        // get profile
    }
}
