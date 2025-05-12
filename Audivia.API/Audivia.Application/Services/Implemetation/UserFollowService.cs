using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.Enums;
using Audivia.Domain.ModelRequests.UserFollow;
using Audivia.Domain.ModelResponses.User;
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
        private readonly IUserRepository _userRepository;

        public UserFollowService(IUserFollowRepository userFollowRepository, IUserRepository userRepository)
        {
            _userFollowRepository = userFollowRepository;
            _userRepository = userRepository;
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

            var existedFollowFromFollowingUser = await _userFollowRepository.FindFirst(u => u.FollowerId == request.FollowingId && u.FollowingId == request.FollowerId);
            
            if (existedFollowFromFollowingUser != null)
            {
                userFollow.AreFriends = true;
                existedFollowFromFollowingUser.AreFriends = true;
                await _userFollowRepository.Update(existedFollowFromFollowingUser);
            }

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

            var existedFollowFromFollowingUser = await _userFollowRepository.FindFirst(u => u.FollowerId == follow.FollowingId && u.FollowingId == follow.FollowerId);
            if (existedFollowFromFollowingUser != null)
            {
                existedFollowFromFollowingUser.AreFriends = false;
                await _userFollowRepository.Update(existedFollowFromFollowingUser);
            }
        }

        public async Task DeleteUserFollow(CreateUserFollowRequest request)
        {
            var follow = await _userFollowRepository.FindFirst(t => t.FollowerId == request.FollowerId && t.FollowingId == request.FollowingId) ?? throw new KeyNotFoundException("Follow not found!");
            await _userFollowRepository.Delete(follow);

            var existedFollowFromFollowingUser = await _userFollowRepository.FindFirst(u => u.FollowerId == follow.FollowingId && u.FollowingId == follow.FollowerId);
            if (existedFollowFromFollowingUser != null)
            {
                existedFollowFromFollowingUser.AreFriends = false;
                await _userFollowRepository.Update(existedFollowFromFollowingUser);
            }
        }

        public async Task<UserFollowStatusResponse> GetUserFollowStatus(GetFollowRequest request)
        {
            var follow = await _userFollowRepository.FindFirst(t => t.FollowerId == request.CurrentUserId && t.FollowingId == request.TargetUserId);
            
            var followBack = await _userFollowRepository.FindFirst(t => t.FollowerId == request.TargetUserId && t.FollowingId == request.CurrentUserId);

            var status = FollowStatus.Following;
            if (follow == null)
            {
                if (followBack != null)
                {
                    status = FollowStatus.NotFollowedBack;
                }
                else
                {
                    status = FollowStatus.NotFollowing;
                }
            } else
            {
                if (followBack != null)
                {
                    status = FollowStatus.Friends;
                }
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

        public async Task<int> CountFriends(string userId)
        {
            FilterDefinition<UserFollow> filter = Builders<UserFollow>.Filter.And(Builders<UserFollow>.Filter.Eq(i => i.FollowerId, userId), Builders<UserFollow>.Filter.Eq(i => i.AreFriends, true));
            int count = await _userFollowRepository.Count(filter);
            return count;
        }

        public async Task<UserShortListResponse> GetFriendsList(string userId)
        {
            FilterDefinition<UserFollow> filter = Builders<UserFollow>.Filter.And(Builders<UserFollow>.Filter.Eq(i => i.FollowerId, userId), Builders<UserFollow>.Filter.Eq(i => i.AreFriends, true));
            
            // lookup User that has Id == FollowingId from User Follow list filtered by above filter
            var userFollows = await _userFollowRepository.Search(filter);
            var followingIds = userFollows.Select(i => i.FollowingId).ToList();
            var users = await _userRepository.Search(Builders<User>.Filter.In(i => i.Id, followingIds));
            
            var userDtos = new List<UserShortDTO>();
            foreach (var user in users.Where(t => !t.IsDeleted))
            {
                var dto = ModelMapper.MapUserToShortDTO(user);
                userDtos.Add(dto);
            }

            // return UserShortListResponse
            return new UserShortListResponse
            {
                Message = "Friend List retrieved successfully!",
                Response = userDtos,
                Success = true
            };

        }
    }
}
