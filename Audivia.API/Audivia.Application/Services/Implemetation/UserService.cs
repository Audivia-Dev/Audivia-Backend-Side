using Audivia.Application.Services.Interface;
using Audivia.Application.Utils.Helper;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.User;
using Audivia.Domain.ModelResponses.User;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<UserResponse> CreateUser(UserCreateRequest request)
        {
            var customerRole = await _roleRepository.GetByRoleName("customer");
            var user = new User
            {
                Email = request.Email,
                Username = request.UserName,
                Password = PasswordHasher.HashPassword(request.Password),
                RoleId = customerRole.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

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
            var tours = await _userRepository.GetAll();
            return tours
            .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapUserToDTO)
                .ToList();
        }

        public async Task<UserResponse> GetUserById(string id)
        {
            var tour = await _userRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (tour == null)
            {
                return new UserResponse
                {
                    Success = false,
                    Message = "User not found",
                    Response = null
                };
            }

            return new UserResponse
            {
                Success = true,
                Message = "User retrieved successfully",
                Response = ModelMapper.MapUserToDTO(tour)
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
            user.Bio = request.Bio ?? user.Bio;
            user.BalanceWallet = request.BalanceWallet ?? user.BalanceWallet;
            user.AudioCharacterId = request.AudioCharacterId ?? user.AudioCharacterId;
            user.AutoPlayDistance = request.AutoPlayDistance ?? user.AutoPlayDistance;
            user.TravelDistance = request.TravelDistance ?? user.TravelDistance;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.Update(user);
        }

        public async Task DeleteUser(string id)
        {
            var tour = await _userRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (tour == null) return;

            tour.IsDeleted = true;
            tour.UpdatedAt = DateTime.UtcNow;

            await _userRepository.Update(tour);
        }

        public async Task<User> GetById(string id)
        {
            return await _userRepository.GetById(new ObjectId(id));
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.Update(user);
        }
    }
}
