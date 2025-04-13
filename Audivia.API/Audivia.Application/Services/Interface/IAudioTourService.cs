
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.AudioTour;
using Audivia.Domain.ModelResponses.AudioTour;

namespace Audivia.Application.Services.Interface
{
    public interface IAudioTourService
    {
        Task<AudioTourResponse> CreateAudioTour(CreateAudioTourRequest request);

        Task<List<AudioTourDTO>> GetAllAudioTours();

        Task<AudioTourResponse> GetAudioTourById(string id);

        Task UpdateAudioTour(string id, UpdateAudioTourRequest request);

        Task DeleteAudioTour(string id);
    }
}
