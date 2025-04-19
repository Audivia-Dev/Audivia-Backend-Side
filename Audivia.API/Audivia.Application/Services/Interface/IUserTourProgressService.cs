using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.UserTourProgress;
using Audivia.Domain.ModelResponses.UserTourProgress;

namespace Audivia.Application.Services.Interface
{
    public interface IUserTourProgressService
    {
        Task<UserTourProgressResponse> CreateUserTourProgress(CreateUserTourProgressRequest request);

        Task<List<UserTourProgressDTO>> GetAllUserTourProgresss();

        Task<UserTourProgressResponse> GetUserTourProgressById(string id);

        Task UpdateUserTourProgress(string id, UpdateUserTourProgressRequest request);

        Task DeleteUserTourProgress(string id);
    }
}
