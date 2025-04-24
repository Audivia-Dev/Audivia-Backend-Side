using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.CheckpointAudio;
using Audivia.Domain.ModelResponses.CheckpointAudio;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class CheckpointAudioService : ICheckpointAudioService
    {
        private readonly ICheckpointAudioRepository _checkpointAudioRepository;

        public CheckpointAudioService(ICheckpointAudioRepository checkpointAudioRepository)
        {
            _checkpointAudioRepository = checkpointAudioRepository;
        }

        public async Task<CheckpointAudioResponse> CreateCheckpointAudio(CreateCheckpointAudioRequest request)
        {
            if ((!string.IsNullOrEmpty(request.AudioCharacterId) && !ObjectId.TryParse(request.AudioCharacterId, out _))
                || !ObjectId.TryParse(request.CheckpointId, out _))
            {
                throw new FormatException("Invalid character Id or checkpoint Id format");
            }
            var checkpointAudio = new CheckpointAudio
            {
                TourCheckpointId = request.CheckpointId,
                AudioCharacterId = request.AudioCharacterId,
                FileUrl = request.FileUrl,
                IsDefault = request.IsDefault,
                Transcript = request.Transcript,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _checkpointAudioRepository.Create(checkpointAudio);

            return new CheckpointAudioResponse
            {
                Success = true,
                Message = "Audio created successfully",
                Response = ModelMapper.MapCheckpointAudioToDTO(checkpointAudio)
            };
        }

        public async Task<List<CheckpointAudioDTO>> GetAllCheckpointAudios()
        {
            var audios = await _checkpointAudioRepository.GetAll();
            return audios
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapCheckpointAudioToDTO)
                .ToList();
        }

        public async Task<CheckpointAudioResponse> GetCheckpointAudioById(string id)
        {
            var audio = await _checkpointAudioRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (audio == null)
            {
                throw new KeyNotFoundException("Checkpoint audio not found!");
            }

            return new CheckpointAudioResponse
            {
                Success = true,
                Message = "Audio retrieved successfully",
                Response = ModelMapper.MapCheckpointAudioToDTO(audio)
            };
        }

        public async Task UpdateCheckpointAudio(string id, UpdateCheckpointAudioRequest request)
        {
            var audio = await _checkpointAudioRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (audio == null) return;

            if ((!string.IsNullOrEmpty(request.AudioCharacterId) && !ObjectId.TryParse(request.AudioCharacterId, out _))
                || !ObjectId.TryParse(request.CheckpointId, out _))
            {
                throw new FormatException("Invalid character Id or checkpoint Id format");
            }
            audio.TourCheckpointId = request.CheckpointId ?? audio.TourCheckpointId;
            audio.AudioCharacterId = request.AudioCharacterId ?? audio.AudioCharacterId;
            audio.FileUrl = request.FileUrl ?? audio.FileUrl;
            audio.IsDefault = request.IsDefault ?? audio.IsDefault;
            audio.Transcript = request.Transcript ?? audio.Transcript;
            audio.UpdatedAt = DateTime.UtcNow;

            await _checkpointAudioRepository.Update(audio);
        }

        public async Task DeleteCheckpointAudio(string id)
        {
            var audio = await _checkpointAudioRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (audio == null) return;

            audio.IsDeleted = true;
            audio.UpdatedAt = DateTime.UtcNow;

            await _checkpointAudioRepository.Update(audio);
        }

    }
}
