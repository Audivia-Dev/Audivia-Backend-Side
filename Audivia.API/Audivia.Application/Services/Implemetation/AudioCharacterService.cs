using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.AudioCharacter;
using Audivia.Domain.ModelResponses.AudioCharacter;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;

namespace Audivia.Application.Services.Implemetation
{
    public class AudioCharacterService : IAudioCharacterService
    {
        private readonly IAudioCharacterRepository _audioCharacterRepository;

        public AudioCharacterService(IAudioCharacterRepository audioCharacterRepository)
        {
            _audioCharacterRepository = audioCharacterRepository;
        }

        public async Task<AudioCharacterResponse> CreateAudioCharacter(CreateAudioCharacterRequest request)
        {
            var audioCharacter = new AudioCharacter
            {
                Description = request.Description,
                Name= request.Name,
                VoiceType = request.VoiceType,
                AvatarUrl = request.AvatarUrl,
                AudioUrl = request.AudioUrl,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _audioCharacterRepository.Create(audioCharacter);

            return new AudioCharacterResponse
            {
                Success = true,
                Message = "Audio character created successfully",
                Response = ModelMapper.MapAudioCharacterToDTO(audioCharacter)
            };
        }

        public async Task<List<AudioCharacterDTO>> GetAllAudioCharacters()
        {
            var characters = await _audioCharacterRepository.GetAll();
            return characters
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapAudioCharacterToDTO)
                .ToList();
        }

        public async Task<AudioCharacterResponse> GetAudioCharacterById(string id)
        {
            var character = await _audioCharacterRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (character == null)
            {
                return new AudioCharacterResponse
                {
                    Success = false,
                    Message = "Audio character not found",
                    Response = null
                };
            }

            return new AudioCharacterResponse
            {
                Success = true,
                Message = "Audio character retrieved successfully",
                Response = ModelMapper.MapAudioCharacterToDTO(character)
            };
        }

        public async Task UpdateAudioCharacter(string id, UpdateAudioCharacterRequest request)
        {
            var character = await _audioCharacterRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (character == null) return;

            character.Description = request.Description ?? character.Description;
            character.Name = request.Name ?? character.Name;
            character.VoiceType = request.VoiceType ?? character.VoiceType;
            character.AvatarUrl = request.AvatarUrl ?? character.AvatarUrl;
            character.AudioUrl = request.AudioUrl ?? character.AudioUrl;
            character.UpdatedAt = DateTime.UtcNow;

            await _audioCharacterRepository.Update(character);
        }

        public async Task DeleteAudioCharacter(string id)
        {
            var character = await _audioCharacterRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (character == null) return;

            character.IsDeleted = true;
            character.UpdatedAt = DateTime.UtcNow;

            await _audioCharacterRepository.Update(character);
        }

    }
}
