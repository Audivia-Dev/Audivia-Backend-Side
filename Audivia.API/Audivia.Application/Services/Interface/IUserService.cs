using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.User;
using Audivia.Domain.ModelResponses.User;
using MongoDB.Driver;
using static MongoDB.Driver.WriteConcern;

namespace Audivia.Application.Services.Interface
{
    public interface IUserService 
    {
        Task<UserResponse> CreateUser(UserCreateRequest request);
        Task<List<UserDTO>> GetAllUsers();
        Task<List<UserDTO>> GetAllMemberAdmin(
           FilterDefinition<User>? filter = null,
           SortDefinition<User>? sortCondition = null,
           int? top = null,
           int? pageIndex = null,
           int? pageSize = null);
        Task<UserResponse> GetUserById(string id);
        Task UpdateUser(string id, UserUpdateRequest request);
        Task DeleteUser(string id);
        Task<User> GetById(string id);
        Task UpdateUser(User user);
        Task IncreaseBalanceAsync(string userId, int ammount);
        Task<bool> DeductBalanceAsync(string userId, double ammount);
    }
}
