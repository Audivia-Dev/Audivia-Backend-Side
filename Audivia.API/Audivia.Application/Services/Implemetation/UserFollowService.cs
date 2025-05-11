using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.Enums;
using Audivia.Domain.ModelRequests.UserFollow;
using Audivia.Domain.ModelResponses.UserFollow;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Audivia.Application.Services.Implemetation
{
    public class UserFollowService : IUserFollowService
    {
        private readonly IUserFollowRepository _userFollowRepository;

        public UserFollowService(IUserFollowRepository userFollowRepository)
        {
            _userFollowRepository = userFollowRepository;
        }

        public async Task<UserFollowResponse> CreateUserFollow(CreateUserFollowRequest request)
        {
            if (!ObjectId.TryParse(request.FollowerId, out _) || !ObjectId.TryParse(request.FollowingId, out _))
            {
                throw new FormatException("Invalid user id format!");
            }

            var existedUserFollow = await _userFollowRepository.FindFirst(u => u.FollowerId == request.FollowerId && u.FollowingId == request.FollowingId);

            if (existedUserFollow != null)
            {
                throw new HttpRequestException("Followed before!");
            }

            var userFollow = new UserFollow
            {
                FollowerId = request.FollowerId,
                FollowingId = request.FollowingId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _userFollowRepository.Create(userFollow);

            return new UserFollowResponse
            {
                Success = true,
                Message = "User follow created successfully",
                Response = ModelMapper.MapUserFollowToDTO(userFollow)
            };
        }

        public async Task<UserFollowListResponse> GetAllUserFollows()
        {
            var follows = await _userFollowRepository.GetAll();
            var followDtos = follows
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapUserFollowToDTO)
                .ToList();
            return new UserFollowListResponse
            {
                Success = true,
                Message = "Follows retrieved successfully",
                Response = followDtos
            };
        }

        public async Task<UserFollowResponse> GetUserFollowById(string id)
        {
            var follow = await _userFollowRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (follow == null)
            {
                throw new KeyNotFoundException("Follow is not existed!");
            }

            return new UserFollowResponse
            {
                Success = true,
                Message = "User follow retrieved successfully",
                Response = ModelMapper.MapUserFollowToDTO(follow)
            };
        }

        public async Task DeleteUserFollow(string id)
        {
            var follow = await _userFollowRepository.FindFirst(t => t.Id == id) ?? throw new KeyNotFoundException("Follow not found!");
            await _userFollowRepository.Delete(follow);
        }

        public async Task DeleteUserFollow(CreateUserFollowRequest request)
        {
            var follow = await _userFollowRepository.FindFirst(t => t.FollowerId == request.FollowerId && t.FollowingId == request.FollowingId) ?? throw new KeyNotFoundException("Follow not found!");
            await _userFollowRepository.Delete(follow);
        }

        public async Task<UserFollowStatusResponse> GetUserFollowStatus(GetFollowRequest request)
        {
            var follow = await _userFollowRepository.FindFirst(t => t.FollowerId == request.CurrentUserId && t.FollowingId == request.TargetUserId);
            if (follow == null) return new UserFollowStatusResponse
            {
                Success = true,
                FollowStatusNumber = (int)FollowStatus.NotFollowing,
                FollowStatusString = FollowStatus.NotFollowing.ToString()
            };

            var status = FollowStatus.Following;
            var followBack = await _userFollowRepository.FindFirst(t => t.FollowerId == request.TargetUserId && t.FollowingId == request.CurrentUserId);
            if (followBack != null)
            {
                status = FollowStatus.Friends;
            }
            return new UserFollowStatusResponse
            {
                Success = true,
                FollowStatusNumber = (int)status,
                FollowStatusString = status.ToString() 
            };
        }

        public async Task<UserFollowListResponse> GetAllUserFollowsByUserId(GetAllUserFollowersByUserIdRequest request)
        {
            if ((!string.IsNullOrEmpty(request.FollowingId) && !ObjectId.TryParse(request.FollowingId, out _)) || (!string.IsNullOrEmpty(request.FollowerId) && !ObjectId.TryParse(request.FollowerId, out _)))
            {
                throw new FormatException("Invalid user id!");
            }

            var filter = Builders<UserFollow>.Filter.And(
                string.IsNullOrEmpty(request.FollowerId) ? Builders<UserFollow>.Filter.Empty : Builders<UserFollow>.Filter.Eq(f => f.FollowerId, request.FollowerId),
                string.IsNullOrEmpty(request.FollowingId) ? Builders<UserFollow>.Filter.Empty : Builders<UserFollow>.Filter.Eq(f => f.FollowingId, request.FollowingId)
            );

            var userFollows = await _userFollowRepository.Search(filter);
            var userFollowDtos = userFollows
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapUserFollowToDTO)
                .ToList();
            return new UserFollowListResponse
            {
                Success = true,
                Message = "UserFollows of user retrieved successfully",
                Response = userFollowDtos
            };
        }

        public async Task<int> CountByFollowerId(string userId)
        {
            FilterDefinition<UserFollow> filter = Builders<UserFollow>.Filter.Eq(i => i.FollowerId, userId);
            int count = await _userFollowRepository.Count(filter);
            return count;
        }

        public async Task<int> CountByFollowingId(string userId)
        {
            FilterDefinition<UserFollow> filter = Builders<UserFollow>.Filter.Eq(i => i.FollowingId, userId);
            int count = await _userFollowRepository.Count(filter);
            return count;
        }

    }
}
