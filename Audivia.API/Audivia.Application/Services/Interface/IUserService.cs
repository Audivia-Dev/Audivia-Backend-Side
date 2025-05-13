using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.User;
using Audivia.Domain.ModelResponses.User;
using static MongoDB.Driver.WriteConcern;

namespace Audivia.Application.Services.Interface
{
    public interface IUserService 
    {
        Task<UserResponse> CreateUser(UserCreateRequest request);
        Task<List<UserDTO>> GetAllUsers();
        Task<UserResponse> GetUserById(string id);
        Task UpdateUser(string id, UserUpdateRequest request);
        Task DeleteUser(string id);
        Task<User> GetById(string id);
        Task UpdateUser(User user);
        Task IncreaseBalanceAsync(string userId, int ammount);
    }
}
