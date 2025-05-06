using Audivia.Application.Services.Interface;
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
            var tour = await _savedTourRepository.FindFirst(t => t.Id == id);
            if (tour == null)
            {
                return new SavedTourResponse
                {
                    Success = false,
                    Message = "Saved tour not found",
                    Response = null
                };
            }

            return new SavedTourResponse
            {
                Success = true,
                Message = "Saved tour retrieved successfully",
                Response = ModelMapper.MapSavedTourToDTO(tour)
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
            .Select(ModelMapper.MapSavedTourToDTO)
            .ToList();
        }
    }
}
