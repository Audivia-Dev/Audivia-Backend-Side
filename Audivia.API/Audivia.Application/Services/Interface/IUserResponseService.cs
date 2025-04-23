using Audivia.Domain.ModelRequests.UserResponse;
using Audivia.Domain.ModelResponses.UserResponse;

namespace Audivia.Application.Services.Interface
{
    public interface IUserResponseService
    {
        Task<UserResponseResponse> CreateUserResponseAsync(CreateUserResponseRequest req);
        Task<UserResponseResponse> UpdateUserResponseAsync(string id, UpdateUserResponseRequest req);
        Task<UserResponseResponse> DeleteUserResponseAsync(string id);
        Task<UserResponseListResponse> GetAllUserResponseAsync();
        Task<UserResponseResponse> GetUserResponseByIdAsync(string id);
    }
}
