using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
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
                return new UserFollowResponse
                {
                    Success = false,
                    Message = "Invalid ID format",
                    Response = null
                };
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
                Message = "Audio follow created successfully",
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
                return new UserFollowResponse
                {
                    Success = false,
                    Message = "Audio follow not found",
                    Response = null
                };
            }

            return new UserFollowResponse
            {
                Success = true,
                Message = "Audio follow retrieved successfully",
                Response = ModelMapper.MapUserFollowToDTO(follow)
            };
        }

        public async Task UpdateUserFollow(string id, UpdateUserFollowRequest request)
        {
            var follow = await _userFollowRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (follow == null) return;

            if (!ObjectId.TryParse(request.FollowerId, out _) || !ObjectId.TryParse(request.FollowingId, out _))
            {
                return;

            }
            follow.FollowerId = request.FollowerId ?? follow.FollowerId;
            follow.FollowingId = request.FollowingId ?? follow.FollowingId;
            follow.UpdatedAt = DateTime.UtcNow;

            await _userFollowRepository.Update(follow);
        }

        public async Task DeleteUserFollow(string id)
        {
            var follow = await _userFollowRepository.FindFirst(t => t.Id == id);
            if (follow == null) throw new KeyNotFoundException("Follow not found!");
            await _userFollowRepository.Delete(follow);
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
