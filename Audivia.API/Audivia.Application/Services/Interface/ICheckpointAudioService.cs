using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.CheckpointAudio;
using Audivia.Domain.ModelResponses.CheckpointAudio;

namespace Audivia.Application.Services.Interface
{
    public interface ICheckpointAudioService
    {
        Task<CheckpointAudioResponse> CreateCheckpointAudio(CreateCheckpointAudioRequest request);

        Task<List<CheckpointAudioDTO>> GetAllCheckpointAudios();

        Task<CheckpointAudioResponse> GetCheckpointAudioById(string id);

        Task<CheckpointAudioResponse> GetCheckpointAudioByTourCheckpointId(string checkpointId, string characterId);

        Task<CheckpointAudioResponse> GetNextAudioByTourCheckpointId(string checkpointId);

        Task<CheckpointAudioResponse> GetPrevAudioAudioByTourCheckpointId(string checkpointId);


        Task UpdateCheckpointAudio(string id, UpdateCheckpointAudioRequest request);

        Task DeleteCheckpointAudio(string id);
    }
}
