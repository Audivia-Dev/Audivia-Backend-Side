using Audivia.Application.Services.Interface;
using Audivia.Application.Utils.QueryBuilders;
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
        private readonly ITourTypeRepository _tourTypeRepository;
        private readonly ITourCheckpointService _tourCheckpointService;

        public TourService(ITourRepository tourRepository, ITourTypeRepository tourTypeRepository, ITourCheckpointService tourCheckpointService)
        {
            _tourRepository = tourRepository;
            _tourTypeRepository = tourTypeRepository;
            _tourCheckpointService = tourCheckpointService;
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

            //tour type is a sub-document 
            audioTour.TourType = await _tourTypeRepository.FindFirst(t => t.Id == request.TypeId);
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
            var filter = TourQueryBuilder.BuildFilter(request);
            var sort = TourQueryBuilder.BuildSort(request.Sort);

            // get tours
            var tours = await _tourRepository.Search(filter, sort, request.Top, request.PageIndex, request.PageSize);
            var dtos = tours
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapAudioTourToDTO)
                .ToList();

            // count all tours with filter
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

            FilterDefinition<TourCheckpoint>? filter = Builders<TourCheckpoint>.Filter.Eq(i => i.TourId, id);
            var tourCheckpoints = await _tourCheckpointService.GetTourCheckpointsAsync(filter);

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

            // update type
            if (!string.IsNullOrEmpty(request.TypeId))
            {
                tour.TypeId = request.TypeId;
                var tourType = await _tourTypeRepository.FindFirst(t => t.Id == request.TypeId);
                tour.TourType = tourType;
            }
            tour.Title = request.Title ?? tour.Title;
            tour.Description = request.Description ?? tour.Description;
            tour.Price = request.Price ?? tour.Price;
            tour.Duration = request.Duration ?? tour.Duration;
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
