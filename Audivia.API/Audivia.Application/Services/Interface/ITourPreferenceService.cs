using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.TourPreference;
using Audivia.Domain.ModelResponses.TourPreference;

namespace Audivia.Application.Services.Interface
{
    public interface ITourPreferenceService 
    {
        Task<TourPreferenceResponse> CreateTourPreference(CreateTourPreferenceRequest request);

        Task<List<TourPreferenceDTO>> GetAllTourPreferences();

        Task<TourPreferenceResponse> GetTourPreferenceById(string id);

        Task UpdateTourPreference(string id, UpdateTourPreferenceRequest request);

        Task DeleteTourPreference(string id);
    }
}
