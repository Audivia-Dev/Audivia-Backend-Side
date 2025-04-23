using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Api;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.AudioTour;
using Audivia.Domain.ModelRequests.Tour;
using Audivia.Domain.ModelResponses.AudioTour;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Audivia.Application.Services.Implemetation
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _tourRepository;
        private readonly ITourCheckpointRepository _tourCheckpointRepository;

        public TourService(ITourRepository tourRepository, ITourCheckpointRepository tourCheckpointRepository)
        {
            _tourRepository = tourRepository;
            _tourCheckpointRepository = tourCheckpointRepository;
        }

        public async Task<AudioTourResponse> CreateAudioTour(CreateTourRequest request)
        {
            if (!ObjectId.TryParse(request.TypeId, out _))
            {
                throw new FormatException("Invalid TypeId format.");
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

        public async Task<AudioTourListResponse> GetAllAudioTours(GetToursRequest request)
        {
            FilterDefinition<Tour>? filter = null; // later
            SortDefinition<Tour>? sort = null;

            if (!string.IsNullOrEmpty(request.Sort))
            {
                switch (request.Sort)
                {
                    case "ratingDesc":
                        sort = Builders<Tour>.Sort.Descending(t => t.AvgRating);
                        break;
                    case "createdAtDesc":
                        sort = Builders<Tour>.Sort.Descending(t => t.CreatedAt);
                        break;
                    default:
                        throw new KeyNotFoundException("Invalid Sorting Criteria!");
                }
            }
            var tours = await _tourRepository.Search(filter, sort, request.Top, request.PageIndex, request.PageSize);
            var dtos = tours
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapAudioTourToDTO)
                .ToList();
            int countAll = await _tourRepository.Count(filter);
            int count = request.Top.HasValue ? request.Top.Value : countAll;
            var pagedResponse = new PaginationResponse<TourDTO>(request.PageIndex ?? 1, request.PageSize ?? 5, count, dtos);
            
            return new AudioTourListResponse
            {
                Success = true,
                Message = "Audio tour list retrieved successfully",
                Response = pagedResponse
            }; 
        }

        public async Task<AudioTourResponse> GetAudioTourById(string id)
        {
            var tour = await _tourRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (tour == null)
            {
                throw new KeyNotFoundException("Audio tour not found!");
            }

            FilterDefinition<TourCheckpoint>? filter = Builders<TourCheckpoint>.Filter.Eq(r => r.TourId, id);
            var tourCheckpoints = await _tourCheckpointRepository.Search(filter, null, null, null, null);

            return new AudioTourResponse
            {
                Success = true,
                Message = "Audio tour retrieved successfully",
                Response = ModelMapper.MapTourDetailsToDTO(tour, tourCheckpoints)
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
