using Audivia.Application.Services.Interface;
using Audivia.Application.Utils.Helper;
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
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;

        public TourService(ITourRepository tourRepository, ITourTypeRepository tourTypeRepository, ITourCheckpointService tourCheckpointService, ITransactionHistoryRepository transactionHistoryRepository)
        {
            _tourRepository = tourRepository;
            _tourTypeRepository = tourTypeRepository;
            _tourCheckpointService = tourCheckpointService;
            _transactionHistoryRepository = transactionHistoryRepository;
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
                Location = request.Location,
                StartLatitude = request.StartLatitude,
                StartLongitude = request.StartLongitude,
                Price = request.Price,
                Duration = request.Duration,
                TypeId = request.TypeId,
                ThumbnailUrl = request.ThumbnailUrl,
                UseCustomMap = request.UseCustomMap,
                CustomMapImages = request.CustomMapImages,
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
                .Select(ModelMapper.MapAudioTourToDTO)
                .ToList();

            // count all tours with filter
            int countAll = await _tourRepository.Count(filter);

            int count = request.Top.HasValue ? request.Top.Value : countAll;

            var pagedResponse = new PaginationResponse<TourDTO>(request.PageIndex ?? 1, request.PageSize ?? count, count, dtos);

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
            var result = tourCheckpoints.OrderBy(t => t.Order).ToList();

            return new AudioTourResponse
            {
                Success = true,
                Message = "Audio tour retrieved successfully",
                Response = ModelMapper.MapTourDetailsToDTO(tour, result)
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
            tour.Location = request.Location ?? tour.Location;
            tour.StartLongitude = request.StartLongitude ?? tour.StartLongitude;
            tour.StartLatitude = request.StartLatitude ?? tour.StartLatitude;
            tour.Price = request.Price ?? tour.Price;
            tour.Duration = request.Duration ?? tour.Duration;
            tour.ThumbnailUrl = request.ThumbnailUrl ?? tour.ThumbnailUrl;
            tour.UseCustomMap = request.UseCustomMap ?? tour.UseCustomMap;
            tour.CustomMapImages = request.CustomMapImages ?? tour.CustomMapImages;
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

        public async Task<AudioTourListResponse> GetSuggestedTour(GetSuggestedTourRequest request)
        {
            // Input validation
            if (request == null || request.Radius <= 0)
            {
                throw new HttpRequestException("Invalid radius!");
            }

            if (request.Latitude < -90 || request.Latitude > 90 || request.Longitude < -180 || request.Longitude > 180)
            {
                throw new HttpRequestException("Invalid coordination!");
            }

            var topTourTypes = await _transactionHistoryRepository.GetTopTourTypesByUserIdAsync(request.UserId, 5);
            var bookedTourIds = await _transactionHistoryRepository.GetBookedTourIdsByUserIdAsync(request.UserId);

            var candidateTours = await _tourRepository.GetToursByTypesExcludingIdsAsync(topTourTypes.Keys.ToList(), bookedTourIds);

            var filteredSortedTours = candidateTours
                .Where(t => t.IsDeleted == false)
                .Select(t =>
                {
                    double distance = DistanceUtils.CalculateDistance(request.Latitude, request.Longitude, t.StartLatitude ?? 0, t.StartLongitude ?? 0);
                    int popularity = topTourTypes.TryGetValue(t.TourType.TourTypeName ?? "", out int count) ? count : 0;

                    return new { Tour = t, Distance = distance, Popularity = popularity };
                })
                //.Where(t => t.Distance <= request.Radius)
                .OrderBy(t => t.Distance)
                .ThenByDescending(t => t.Popularity)
                .Take(request.Top)
                .Select(t => t.Tour)
                .ToList();


            var totalCount = filteredSortedTours.Count;

            // Map to DTOs
            var dtos = filteredSortedTours.Select(ModelMapper.MapAudioTourToDTO).ToList();

            // Pagination response
            var pagedResponse = new PaginationResponse<TourDTO>(1, 3, totalCount, dtos);

            return new AudioTourListResponse
            {
                Success = true,
                Message = "Audio tour list retrieved successfully",
                Response = pagedResponse
            };

        }
    }
}
