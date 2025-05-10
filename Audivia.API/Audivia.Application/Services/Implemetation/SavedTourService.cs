using Audivia.Application.Services.Interface;
using Audivia.Application.Utils.Helper;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.SavedTour;
using Audivia.Domain.ModelResponses.SavedTour;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class SavedTourService : ISavedTourService
    {
        private readonly ISavedTourRepository _savedTourRepository;

        public SavedTourService(ISavedTourRepository savedTourRepository)
        {
            _savedTourRepository = savedTourRepository;
        }

        public async Task<SavedTourResponse> CreateSavedTour(CreateSavedTourRequest request)
        {
            if (!ObjectId.TryParse(request.TourId, out _) || !ObjectId.TryParse(request.UserId, out _))
            {
                return new SavedTourResponse
                {
                    Success = false,
                    Message = "Invalid Tour Id or User Id format",
                    Response = null
                };
            }
            var existingSavedTour = await _savedTourRepository.FindByUserIdAndTourIdAsync(request.UserId, request.TourId);
            if (existingSavedTour != null)
            {
                return new SavedTourResponse
                {
                    Success = false,
                    Message = "Tour has already been saved by this user",
                    Response = null
                };
            }
            var savedTour = new SavedTour
            {
                TourId = request.TourId,
                UserId = request.UserId,
                PlannedTime = request.PlannedTime,
                SavedAt = DateTime.UtcNow,
            };

            await _savedTourRepository.Create(savedTour);

            return new SavedTourResponse
            {
                Success = true,
                Message = "Saved tour created successfully",
                Response = ModelMapper.MapSavedTourToDTO(savedTour)
            };
        }

        public async Task<List<SavedTourDTO>> GetAllSavedTours()
        {
            var tours = await _savedTourRepository.GetAll();
            return tours
                .Select(ModelMapper.MapSavedTourToDTO)
                .ToList();
        }

        public async Task<SavedTourResponse> GetSavedTourById(string id)
        {
            var tour = await _savedTourRepository.GetByIdWithTour(id);
            if (tour == null)
            {
                return new SavedTourResponse
                {
                    Success = false,
                    Message = "Saved tour not found",
                    Response = null
                };
            }
            var dto = ModelMapper.MapSavedTourToDTO(tour);
            dto.TimeAgo = TimeUtils.GetTimeElapsed((DateTime)tour.SavedAt);
            return new SavedTourResponse
            {
                Success = true,
                Message = "Saved tour retrieved successfully",
                Response = dto
            };
        }

        public async Task UpdateSavedTour(string id, UpdateSavedTourRequest request)
        {
            var tour = await _savedTourRepository.FindFirst(t => t.Id == id);
            if (tour == null) return;

            tour.PlannedTime = request.PlannedTime ?? tour.PlannedTime;

            await _savedTourRepository.Update(tour);
        }

        public async Task DeleteSavedTour(string id)
        {
            var tour = await _savedTourRepository.FindFirst(t => t.Id == id);
            if (tour == null) return;
            await _savedTourRepository.Delete(tour);
        }

        public async Task<List<SavedTourDTO>> GetSavedTourByUserId(string userId)
        {
            var savedTours = await _savedTourRepository.GetSavedTourByUserId(userId);
            return savedTours
            .Select(t =>
            {
                var dto = ModelMapper.MapSavedTourToDTO(t);
                dto.TimeAgo = TimeUtils.GetTimeElapsed((DateTime)t.SavedAt);
                return dto;
            }).ToList();

        }
    }
}
