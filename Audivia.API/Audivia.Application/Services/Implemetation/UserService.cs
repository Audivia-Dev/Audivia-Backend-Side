using Audivia.Application.Services.Interface;
using Audivia.Application.Utils.Helper;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.User;
using Audivia.Domain.ModelResponses.User;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Audivia.Application.Services.Implemetation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserFollowService _userFollowService;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IUserFollowService userFollowService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userFollowService = userFollowService;
        }

        public async Task<UserResponse> CreateUser(UserCreateRequest request)
        {
            var role = await _roleRepository.GetByRoleName(request.RoleName) 
                            ?? throw new KeyNotFoundException("Role not found!");
            
            var user = new User
            {
                Email = request.Email,
                Username = request.UserName,
                Password = PasswordHasher.HashPassword(request.Password),
                RoleId = role.Id,
                BirthDay = request.BirthDay,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            if (role.RoleName.ToLower().Equals("admin") || role.RoleName.ToLower().Equals("staff"))
            {
                user.ConfirmedEmail = true;
            }

            await _userRepository.Create(user);

            return new UserResponse
            {
                Success = true,
                Message = "User created successfully",
                Response = ModelMapper.MapUserToDTO(user)
            };
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            var userDtos = new List<UserDTO>();

            foreach (var user in users.Where(t => !t.IsDeleted))
            {
                var dto = await FinalMapUserToDTO(user);
                userDtos.Add(dto);
            }
            return userDtos;
        }
        public async Task<List<UserDTO>> GetAllMemberAdmin(
           FilterDefinition<User>? filter = null,
           SortDefinition<User>? sortCondition = null,
           int? top = null,
           int? pageIndex = null,
           int? pageSize = null)
        {
            var finalFilter = filter ?? Builders<User>.Filter.Eq(u => u.IsDeleted, false);

            var users = await _userRepository.Search(
                filter: finalFilter,
                sortCondition: sortCondition,
                top: top,
                pageIndex: pageIndex,
                pageSize: pageSize
            );

            var userDtos = new List<UserDTO>();
            foreach (var user in users)
            {
                var dto = await FinalMapUserToDTO(user);
                userDtos.Add(dto);
            }

            return userDtos;
        }
        public async Task<UserResponse> GetUserById(string id)
        {
            var user = await _userRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }
            
            return new UserResponse
            {
                Success = true,
                Message = "User retrieved successfully",
                Response = await FinalMapUserToDTO(user)
            };
        }

        public async Task UpdateUser(string id, UserUpdateRequest request)
        {
            var user = await _userRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (user == null) return;

            if (string.IsNullOrWhiteSpace(request.AudioCharacterId))
            {
                request.AudioCharacterId = null;
            }
            if (request.AudioCharacterId != null && !ObjectId.TryParse(request.AudioCharacterId, out _))
            {
                throw new FormatException("Invalid audio character id value!");
            }

            user.FullName = request.FullName ?? user.FullName;
            user.Phone = request.Phone ?? user.Phone;
            user.AvatarUrl = request.AvatarUrl ?? user.AvatarUrl;
            user.CoverPhoto = request.CoverPhoto ?? user.CoverPhoto;
            user.Bio = request.Bio ?? user.Bio;
            user.BalanceWallet = request.BalanceWallet ?? user.BalanceWallet;
            user.AudioCharacterId = request.AudioCharacterId ?? user.AudioCharacterId;
            user.AutoPlayDistance = request.AutoPlayDistance ?? user.AutoPlayDistance;
            user.TravelDistance = request.TravelDistance ?? user.TravelDistance;
            user.BirthDay = request.BirthDay ?? user.BirthDay;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.Update(user);
        }

        public async Task DeleteUser(string id)
        {
            var user = await _userRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (user == null) return;

            user.IsDeleted = true;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.Update(user);
        }

        public async Task<User> GetById(string id)
        {
            return await _userRepository.GetById(new ObjectId(id));
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.Update(user);
        }

        private async Task<UserDTO> FinalMapUserToDTO(User user)
        {
            var role = await _roleRepository.FindFirst(role => role.Id == user.RoleId)
                            ?? throw new KeyNotFoundException("Role not found!");

            if (role.RoleName.ToLower().Equals("customer"))
            {
                int followers = await _userFollowService.CountByFollowingId(user.Id);
                int following = await _userFollowService.CountByFollowerId(user.Id);
                int friends = await _userFollowService.CountFriends(user.Id);
                return ModelMapper.MapUserToDTO(user, role.RoleName, followers, following, friends);
            }
            return ModelMapper.MapUserToDTO(user, role.RoleName);
        }
        public async Task IncreaseBalanceAsync(string userId, int amount)
        {
            var user = await _userRepository.FindFirst(t => t.Id == userId && !t.IsDeleted);
            if (user == null) return;
            user.BalanceWallet += amount;
            await _userRepository.Update(user);
        }

        public async Task<bool> DeductBalanceAsync(string userId, double amount)
        {
            var user = await _userRepository.FindFirst(t => t.Id == userId && !t.IsDeleted);
            if (user == null) return false;
            user.BalanceWallet -= amount;
            await _userRepository.Update(user);
            return true;
        }
    }
}
