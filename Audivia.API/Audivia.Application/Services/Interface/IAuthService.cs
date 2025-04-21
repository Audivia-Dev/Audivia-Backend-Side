using Audivia.Domain.ModelRequests.Auth;
using Audivia.Domain.ModelResponses.Auth;

namespace Audivia.Application.Services.Interface
{
    public interface IAuthService
    {
        // login
        Task<LoginResponse> LoginWithEmailAndPassword(LoginRequest request);

        // logout

        // register

        // get profile
    }
}
