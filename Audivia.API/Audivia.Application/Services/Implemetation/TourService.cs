using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.AudioTour;
using Audivia.Domain.ModelResponses.AudioTour;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _tourRepository;

        public TourService(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        public async Task<AudioTourResponse> CreateAudioTour(CreateTourRequest request)
        {
            if (!ObjectId.TryParse(request.TypeId, out _))
            {
                return new AudioTourResponse
                {
                    Success = false,
                    Message = "Invalid TypeId format",
                    Response = null
                };
            }
            var audioTour = new Tour
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Duration = request.Duration,
                TypeId = request.TypeId,
                ThumbnailUrl = request.ThumbnailUrl,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _tourRepository.Create(audioTour);

            return new AudioTourResponse
            {
                Success = true,
                Message = "Audio tour created successfully",
                Response = ModelMapper.MapAudioTourToDTO(audioTour)
            };
        }

        public async Task<List<TourDTO>> GetAllAudioTours()
        {
            var tours = await _tourRepository.GetAll();
            return tours
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapAudioTourToDTO)
                .ToList();
        }

        public async Task<AudioTourResponse> GetAudioTourById(string id)
        {
            var tour = await _tourRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (tour == null)
            {
                return new AudioTourResponse
                {
                    Success = false,
                    Message = "Audio tour not found",
                    Response = null
                };
            }

            return new AudioTourResponse
            {
                Success = true,
                Message = "Audio tour retrieved successfully",
                Response = ModelMapper.MapAudioTourToDTO(tour)
            };
        }

        public async Task UpdateAudioTour(string id, UpdateTourRequest request)
        {
            var tour = await _tourRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (tour == null) return;

            if (!string.IsNullOrEmpty(request.TypeId) && !ObjectId.TryParse(request.TypeId, out _))
            {
                throw new FormatException("Invalid TypeId format.");
            }
            tour.Title = request.Title ?? tour.Title;
            tour.Description = request.Description ?? tour.Description;
            tour.Price = request.Price ?? tour.Price;
            tour.Duration = request.Duration ?? tour.Duration;
            tour.TypeId = request.TypeId ?? tour.TypeId;
            tour.ThumbnailUrl = request.ThumbnailUrl ?? tour.ThumbnailUrl;
            tour.UpdatedAt = DateTime.UtcNow;

            await _tourRepository.Update(tour);
        }

        public async Task DeleteAudioTour(string id)
        {
            var tour = await _tourRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (tour == null) return;

            tour.IsDeleted = true;
            tour.UpdatedAt = DateTime.UtcNow;

            await _tourRepository.Update(tour);
        }

        
    }
}
