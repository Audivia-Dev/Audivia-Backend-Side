using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.TourReview;
using Audivia.Domain.ModelResponses.TourReview;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Audivia.Application.Services.Implemetation
{
    public class TourReviewService : ITourReviewService
    {
        private readonly ITourReviewRepository _tourReviewRepository;
        private readonly ITourRepository _tourRepository;

        public TourReviewService(ITourReviewRepository tourReviewRepository, ITourRepository tourRepository)
        {
            _tourReviewRepository = tourReviewRepository;
            _tourRepository = tourRepository;
        }

        public async Task<TourReviewResponse> CreateTourReview(CreateTourReviewRequest request)
        {
            if (!ObjectId.TryParse(request.CreatedBy, out _))
            {
                throw new FormatException("Invalid created by value!");
            }

            Tour tour = await _tourRepository.FindFirst(t => t.Id == request.TourId && !t.IsDeleted) ?? throw new KeyNotFoundException("Tour not found!");

            TourReview? existedReview = await _tourReviewRepository.FindFirst(t => t.TourId == request.TourId && t.CreatedBy == request.CreatedBy);
            if (existedReview != null)
            {
                throw new HttpRequestException("You reviewed this tour!");
            }

            var tourReview = new TourReview
            {
                TourId = request.TourId,
                Title = request.Title,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                Rating = request.Rating,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _tourReviewRepository.Create(tourReview);

            // update avg rating of the reviewed tour            
            if (request.Rating.HasValue)
            {
                tour.AvgRating = await CalculateAverageRatingAsync(tour.Id);
                await _tourRepository.Update(tour);
            }

            return new TourReviewResponse
            {
                Success = true,
                Message = "TourReview created successfully",
                Response = ModelMapper.MapTourReviewToDTO(tourReview)
            };
        }

        public async Task<TourReviewListResponse> GetAllTourReviews()
        {
            var tourReviews = await _tourReviewRepository.GetAll();
            var tourReviewDtos = tourReviews
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapTourReviewToDTO)
                .ToList();
            return new TourReviewListResponse
            {
                Success = true,
                Message = "TourReviews retrieved successfully",
                Response = tourReviewDtos
            };
        }

        public async Task<TourReviewResponse> GetTourReviewById(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid tour review id!");
            }
            var tourReview = await _tourReviewRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (tourReview == null)
            {
                throw new KeyNotFoundException("Tour review not found!");
            }

            return new TourReviewResponse
            {
                Success = true,
                Message = "TourReview retrieved successfully",
                Response = ModelMapper.MapTourReviewToDTO(tourReview)
            };
        }

        public async Task UpdateTourReview(string id, UpdateTourReviewRequest request)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid tour review id!");
            }
            var tourReview = await _tourReviewRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (tourReview == null) throw new KeyNotFoundException("TourReview not found!");

            if (!string.IsNullOrEmpty(request.UpdatedBy) && (!ObjectId.TryParse(request.UpdatedBy, out _) || !request.UpdatedBy.Equals(tourReview.CreatedBy)))
            {
                throw new FormatException("Invalid user try to update tourReview");
            }

            Tour tour = await _tourRepository.FindFirst(t => t.Id == tourReview.TourId && !t.IsDeleted) ?? throw new KeyNotFoundException("Tour not found!");

            tourReview.Title = request.Title ?? tourReview.Title;
            tourReview.Content = request.Content ?? tourReview.Content;
            tourReview.ImageUrl = request.ImageUrl ?? tourReview.ImageUrl;
            tourReview.Rating = request.Rating ?? tourReview.Rating;
            tourReview.UpdatedAt = DateTime.UtcNow;

            await _tourReviewRepository.Update(tourReview);

            // update average rating of the reviewed tour
            if (request.Rating.HasValue)
            {
                tour.AvgRating = await CalculateAverageRatingAsync(tour.Id);
                await _tourRepository.Update(tour);
            }
        }

        public async Task DeleteTourReview(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid tour review id!");
            }
            var tourReview = await _tourReviewRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (tourReview == null) throw new KeyNotFoundException("TourReview not found!");

            tourReview.IsDeleted = true;
            tourReview.UpdatedAt = DateTime.UtcNow;

            await _tourReviewRepository.Update(tourReview);
        }

        private async Task<double> CalculateAverageRatingAsync(string tourId)
        {
            FilterDefinition<TourReview>? filter = Builders<TourReview>.Filter.Eq(r => r.TourId, tourId);
            var tourReviews = await _tourReviewRepository.Search(filter);
            var validRatings = tourReviews
                                .Where(r => r.Rating.HasValue)
                                .Select(r => r.Rating!.Value)
                                .ToList();

            if (!validRatings.Any())
                return 0;

            return validRatings.Average();
        }
    }
}
