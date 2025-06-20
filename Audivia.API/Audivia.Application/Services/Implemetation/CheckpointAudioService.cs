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
        private readonly ITourCheckpointRepository _tourCheckpointRepository;

        public CheckpointAudioService(
            ICheckpointAudioRepository checkpointAudioRepository,
            ITourCheckpointRepository tourCheckpointRepository)
        {
            _checkpointAudioRepository = checkpointAudioRepository;
            _tourCheckpointRepository = tourCheckpointRepository;
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
                VideoUrl = request.VideoUrl,
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
                || (!string.IsNullOrEmpty(request.CheckpointId) && !ObjectId.TryParse(request.CheckpointId, out _)))
            {
                throw new FormatException("Invalid character Id or checkpoint Id format");
            }
            audio.TourCheckpointId = request.CheckpointId ?? audio.TourCheckpointId;
            audio.AudioCharacterId = request.AudioCharacterId ?? audio.AudioCharacterId;
            audio.FileUrl = request.FileUrl ?? audio.FileUrl;
            audio.VideoUrl = request.VideoUrl ?? audio.VideoUrl;
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

        public async Task<CheckpointAudioResponse> GetCheckpointAudioByTourCheckpointId(string checkpointId, string characterId)
        {
            var audio =  await _checkpointAudioRepository.FindFirst(t => t.TourCheckpointId == checkpointId
            && t.AudioCharacterId == characterId
            && !t.IsDeleted);
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

        public async Task<CheckpointAudioResponse?> GetPrevAudioAudioByTourCheckpointId(string checkpointId)
        {
            var currentCheckpoint = await _tourCheckpointRepository.FindFirst(tc => tc.Id == checkpointId && !tc.IsDeleted);
            if (currentCheckpoint == null)
                throw new KeyNotFoundException("Checkpoint not found!");
            int currentOrder = currentCheckpoint.Order;

            // Find previous checkpoint
            TourCheckpoint? prevCheckpoint = null;
            int i = 1;
            while (prevCheckpoint == null && i <= currentOrder)
            {
                prevCheckpoint = await _tourCheckpointRepository.FindFirst(tc => tc.TourId == currentCheckpoint.TourId && !tc.IsDeleted && tc.Order == currentOrder - i);
                i++;
                if (prevCheckpoint != null)
                {
                    break;
                }
            }

            if (prevCheckpoint == null)
                throw new KeyNotFoundException("Prev checkpoint not found!");

            var audio = await _checkpointAudioRepository.FindFirst(a => a.TourCheckpointId == prevCheckpoint.Id && !a.IsDeleted);
            if (audio == null)
                throw new KeyNotFoundException("Audio for previous checkpoint not found!");

            return new CheckpointAudioResponse
            {
                Success = true,
                Message = "Audio retrieved successfully",
                Response = ModelMapper.MapCheckpointAudioToDTO(audio)
            };
        }

        public async Task<CheckpointAudioResponse?> GetNextAudioByTourCheckpointId(string checkpointId)
        {
            var currentCheckpoint = await _tourCheckpointRepository.FindFirst(tc => tc.Id == checkpointId && !tc.IsDeleted);
            if (currentCheckpoint == null)
                throw new KeyNotFoundException("Checkpoint not found!");
            int currentOrder = currentCheckpoint.Order;

            // Find next checkpoint
            TourCheckpoint? nextCheckpoint = null;
            int i = 1;
            while (nextCheckpoint == null && i <= 5)
            {
                nextCheckpoint = await _tourCheckpointRepository.FindFirst(tc => tc.TourId == currentCheckpoint.TourId && !tc.IsDeleted && tc.Order == currentCheckpoint.Order + i);
                i++;
                if (nextCheckpoint != null)
                {
                    break;
                }
            }

            if (nextCheckpoint == null)
                throw new KeyNotFoundException("Next checkpoint not found!");

            var audio = await _checkpointAudioRepository.FindFirst(a => a.TourCheckpointId == nextCheckpoint.Id && !a.IsDeleted);
            if (audio == null)
                throw new KeyNotFoundException("Audio for next checkpoint not found!");

            return new CheckpointAudioResponse
            {
                Success = true,
                Message = "Audio retrieved successfully",
                Response = ModelMapper.MapCheckpointAudioToDTO(audio)
            };
        }
    }
}
