using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.UserFollow;
using Audivia.Domain.ModelResponses.UserFollow;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

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

        public async Task<List<UserFollowDTO>> GetAllUserFollows()
        {
            var follows = await _userFollowRepository.GetAll();
            return follows
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapUserFollowToDTO)
                .ToList();
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
            var follow = await _userFollowRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (follow == null) return;

            follow.IsDeleted = true;
            follow.UpdatedAt = DateTime.UtcNow;

            await _userFollowRepository.Update(follow);
        }

    }
}
