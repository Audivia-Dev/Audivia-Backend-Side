using Audivia.Domain.ModelRequests.User;
using Audivia.Domain.ModelResponses.User;

namespace Audivia.Application.Services.Interface
{
    public interface IUserService 
    {
        Task<UserResponse> CreateAccount(UserCreateRequest request);
      
    }
}
