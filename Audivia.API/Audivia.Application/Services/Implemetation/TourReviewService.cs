using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.TourReview;
using Audivia.Domain.ModelResponses.TourReview;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

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
            Tour tour = await _tourRepository.GetById(request.TourId) ?? throw new KeyNotFoundException("Tour not found!");
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

            //tour.AvgRating = 
            await _tourReviewRepository.Create(tourReview);

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
            tourReview.Title = request.Title ?? tourReview.Title;
            tourReview.Content = request.Content ?? tourReview.Content;
            tourReview.ImageUrl = request.ImageUrl ?? tourReview.ImageUrl;
            tourReview.Rating = request.Rating ?? tourReview.Rating;
            tourReview.UpdatedAt = DateTime.UtcNow;

            await _tourReviewRepository.Update(tourReview);
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
    }
}
