using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.UserAudioTour;
using Audivia.Domain.ModelResponses.UserAudioTour;

namespace Audivia.Application.Services.Interface
{
    public interface IUserAudioTourService
    {
        Task<UserAudioTourResponse> CreateUserAudioTour(CreateUserAudioTourRequest request);

        Task<List<UserAudioTourDTO>> GetAllUserAudioTours();

        Task<UserAudioTourResponse> GetUserAudioTourById(string id);

        Task UpdateUserAudioTour(string id, UpdateUserAudioTourRequest request);

        Task DeleteUserAudioTour(string id);
    }
}
