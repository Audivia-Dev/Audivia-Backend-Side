
using Audivia.Domain.Commons.Api;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.AudioTour;
using Audivia.Domain.ModelRequests.Tour;
using Audivia.Domain.ModelResponses.AudioTour;

namespace Audivia.Application.Services.Interface
{
    public interface ITourService
    {
        Task<AudioTourResponse> CreateAudioTour(CreateTourRequest request);

        Task<AudioTourListResponse> GetAllAudioTours(GetToursRequest request);

        Task<AudioTourResponse> GetAudioTourById(string id);

        Task<AudioTourListResponse> GetSuggestedTour(GetSuggestedTourRequest request);

        Task UpdateAudioTour(string id, UpdateTourRequest request);

        Task DeleteAudioTour(string id);
    }
}
