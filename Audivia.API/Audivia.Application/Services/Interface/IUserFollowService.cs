using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.UserFollow;
using Audivia.Domain.ModelResponses.UserFollow;

namespace Audivia.Application.Services.Interface
{
    public interface IUserFollowService
    {
        Task<UserFollowResponse> CreateUserFollow(CreateUserFollowRequest request);

        Task<List<UserFollowDTO>> GetAllUserFollows();

        Task<UserFollowResponse> GetUserFollowById(string id);

        Task UpdateUserFollow(string id, UpdateUserFollowRequest request);

        Task DeleteUserFollow(string id);
    }
}
