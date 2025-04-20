
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.AudioTour;
using Audivia.Domain.ModelResponses.AudioTour;

namespace Audivia.Application.Services.Interface
{
    public interface ITourService
    {
        Task<AudioTourResponse> CreateAudioTour(CreateTourRequest request);

        Task<List<TourDTO>> GetAllAudioTours();

        Task<AudioTourResponse> GetAudioTourById(string id);

        Task UpdateAudioTour(string id, UpdateTourRequest request);

        Task DeleteAudioTour(string id);
    }
}
