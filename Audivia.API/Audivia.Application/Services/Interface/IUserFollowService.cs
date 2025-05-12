using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.UserFollow;
using Audivia.Domain.ModelResponses.User;
using Audivia.Domain.ModelResponses.UserFollow;

namespace Audivia.Application.Services.Interface
{
    public interface IUserFollowService
    {
        Task<UserFollowResponse> CreateUserFollow(CreateUserFollowRequest request);
        Task<UserFollowListResponse> GetAllUserFollows();
        Task<UserFollowResponse> GetUserFollowById(string id);
        Task DeleteUserFollow(string id);
        Task DeleteUserFollow(CreateUserFollowRequest request);
        Task<UserFollowStatusResponse> GetUserFollowStatus(GetFollowRequest request);
        Task<UserFollowListResponse> GetAllUserFollowsByUserId (GetAllUserFollowersByUserIdRequest request);
        Task<int> CountByFollowerId(string userId);
        Task<int> CountByFollowingId(string userId);
        Task<int> CountFriends(string userId); // friends: follow 2 chieu
        Task<UserShortListResponse> GetFriendsList(string userId);
    }
}
