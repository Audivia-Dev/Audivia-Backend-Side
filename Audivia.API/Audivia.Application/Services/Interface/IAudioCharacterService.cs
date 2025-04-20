using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.AudioCharacter;
using Audivia.Domain.ModelResponses.AudioCharacter;

namespace Audivia.Application.Services.Interface
{
    public interface IAudioCharacterService
    {
        Task<AudioCharacterResponse> CreateAudioCharacter(CreateAudioCharacterRequest request);

        Task<List<AudioCharacterDTO>> GetAllAudioCharacters();

        Task<AudioCharacterResponse> GetAudioCharacterById(string id);

        Task UpdateAudioCharacter(string id, UpdateAudioCharacterRequest request);

        Task DeleteAudioCharacter(string id);
    }
}
