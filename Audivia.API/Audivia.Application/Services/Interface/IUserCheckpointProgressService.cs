using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.UserCheckpointProgress;
using Audivia.Domain.ModelResponses.UserCheckpointProgress;

namespace Audivia.Application.Services.Interface
{
    public interface IUserCheckpointProgressService
    {
        Task<UserCheckpointProgressResponse> CreateUserCheckpointProgress(CreateUserCheckpointProgressRequest request);

        Task<List<UserCheckpointProgressDTO>> GetAllUserCheckpointProgresss();

        Task<UserCheckpointProgressResponse> GetUserCheckpointProgressById(string id);
        Task<UserCheckpointProgressResponse> GetByTourProgressAndCheckpoint(string tourProgressId, string checkpointId);

        Task UpdateUserCheckpointProgress(string id, UpdateUserCheckpointProgressRequest request);

        Task DeleteUserCheckpointProgress(string id);
    }
}
