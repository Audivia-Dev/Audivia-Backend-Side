using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.SavedTour;
using Audivia.Domain.ModelResponses.SavedTour;

namespace Audivia.Application.Services.Interface
{
    public interface ISavedTourService
    {
        Task<SavedTourResponse> CreateSavedTour(CreateSavedTourRequest request);

        Task<List<SavedTourDTO>> GetAllSavedTours();

        Task<SavedTourResponse> GetSavedTourById(string id);

        Task UpdateSavedTour(string id, UpdateSavedTourRequest request);

        Task DeleteSavedTour(string id);
        Task<List<SavedTourDTO>> GetSavedTourByUserId(string userId);
    }
}
