using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.TourPreference;
using Audivia.Domain.ModelResponses.TourPreference;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class TourPreferenceService : ITourPreferenceService
    {
        private readonly ITourPreferenceRepository _tourPreferenceRepository;

        public TourPreferenceService(ITourPreferenceRepository tourPreferenceRepository)
        {
            _tourPreferenceRepository = tourPreferenceRepository;
        }

        public async Task<TourPreferenceResponse> CreateTourPreference(CreateTourPreferenceRequest request)
        {
            if (!ObjectId.TryParse(request.UserId, out _) && !ObjectId.TryParse(request.TourId, out _))
            {
                return new TourPreferenceResponse
                {
                    Success = false,
                    Message = "Invalid UserId or TourId format",
                    Response = null
                };
            }
            var tourPreference = new TourPreference
            {
                UserId = request.UserId,
                TourId = request.TourId,
                PredictedScore = request.PredictedScore,
                CreatedAt = DateTime.UtcNow,
            };

            await _tourPreferenceRepository.Create(tourPreference);

            return new TourPreferenceResponse
            {
                Success = true,
                Message = "Audio tour created successfully",
                Response = ModelMapper.MapTourPreferenceToDTO(tourPreference)
            };
        }

        public async Task<List<TourPreferenceDTO>> GetAllTourPreferences()
        {
            var tours = await _tourPreferenceRepository.GetAll();
            return tours
                .Select(ModelMapper.MapTourPreferenceToDTO)
                .ToList();
        }

        public async Task<TourPreferenceResponse> GetTourPreferenceById(string id)
        {
            var tour = await _tourPreferenceRepository.FindFirst(t => t.Id == id);
            if (tour == null)
            {
                return new TourPreferenceResponse
                {
                    Success = false,
                    Message = "Tour Preference not found",
                    Response = null
                };
            }

            return new TourPreferenceResponse
            {
                Success = true,
                Message = "Tour Preference retrieved successfully",
                Response = ModelMapper.MapTourPreferenceToDTO(tour)
            };
        }

        public async Task UpdateTourPreference(string id, UpdateTourPreferenceRequest request)
        {
            var tour = await _tourPreferenceRepository.FindFirst(t => t.Id == id);
            if (tour == null) return;

            tour.PredictedScore = request.PredictedScore;

            await _tourPreferenceRepository.Update(tour);
        }

        public async Task DeleteTourPreference(string id)
        {
            var tour = await _tourPreferenceRepository.FindFirst(t => t.Id == id);
            if (tour == null) return;

            await _tourPreferenceRepository.Delete(tour);
        }

    }
}
