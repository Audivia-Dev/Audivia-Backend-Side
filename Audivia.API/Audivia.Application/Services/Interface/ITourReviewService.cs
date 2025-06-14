using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.TourReview;
using Audivia.Domain.ModelResponses.TourReview;
using Audivia.Domain.Models;

namespace Audivia.Application.Services.Interface
{
    public interface ITourReviewService
    {
        Task<TourReviewResponse> CreateTourReview(CreateTourReviewRequest request);

        Task<TourReviewListResponse> GetAllTourReviews();

        Task<TourReviewResponse> GetTourReviewById(string id);

        Task UpdateTourReview(string id, UpdateTourReviewRequest request);

        Task DeleteTourReview(string id);
        Task<List<TourReviewDTO>> GetReviewsByTourId(string tourId);
        Task<TourReviewResponse> GetReviewsByTourIdAndUserId(string tourId, string userId);
    }
}
