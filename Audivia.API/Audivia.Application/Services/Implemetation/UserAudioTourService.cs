using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.UserAudioTour;
using Audivia.Domain.ModelResponses.UserAudioTour;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class UserAudioTourService : IUserAudioTourService
    {
        private readonly IUserAudioTourRepository _userAudioTourRepository;

        public UserAudioTourService(IUserAudioTourRepository userAudioTourRepository)
        {
            _userAudioTourRepository = userAudioTourRepository;
        }

        public async Task<UserAudioTourResponse> CreateUserAudioTour(CreateUserAudioTourRequest request)
        {
            if (!ObjectId.TryParse(request.UserId, out _) || !ObjectId.TryParse(request.TourId, out _))
            {
                return new UserAudioTourResponse
                {
                    Success = false,
                    Message = "Invalid UserId or TourId format",
                    Response = null
                };
            }
            var userAudioTour = new UserAudioTour
            {
                UserId = request.UserId,
                TourId = request.TourId,
                IsCompleted = request.IsCompleted,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _userAudioTourRepository.Create(userAudioTour);

            return new UserAudioTourResponse
            {
                Success = true,
                Message = "Audio userTour created successfully",
                Response = ModelMapper.MapUserAudioTourToDTO(userAudioTour)
            };
        }

        public async Task<List<UserAudioTourDTO>> GetAllUserAudioTours()
        {
            var userTours = await _userAudioTourRepository.GetAll();
            return userTours
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapUserAudioTourToDTO)
                .ToList();
        }

        public async Task<UserAudioTourResponse> GetUserAudioTourById(string id)
        {
            var userTour = await _userAudioTourRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (userTour == null)
            {
                return new UserAudioTourResponse
                {
                    Success = false,
                    Message = "Audio userTour not found",
                    Response = null
                };
            }

            return new UserAudioTourResponse
            {
                Success = true,
                Message = "Audio userTour retrieved successfully",
                Response = ModelMapper.MapUserAudioTourToDTO(userTour)
            };
        }

        public async Task UpdateUserAudioTour(string id, UpdateUserAudioTourRequest request)
        {
            var userTour = await _userAudioTourRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (userTour == null) return;
            userTour.IsCompleted = request.IsCompleted ?? userTour.IsCompleted;
            userTour.UpdatedAt = DateTime.UtcNow;

            await _userAudioTourRepository.Update(userTour);
        }

        public async Task DeleteUserAudioTour(string id)
        {
            var userTour = await _userAudioTourRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (userTour == null) return;

            userTour.IsDeleted = true;
            userTour.UpdatedAt = DateTime.UtcNow;

            await _userAudioTourRepository.Update(userTour);
        }

    }
}
