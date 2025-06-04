using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.UserCheckpointProgress;
using Audivia.Domain.ModelResponses.UserCheckpointProgress;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class UserCheckpointProgressService : IUserCheckpointProgressService
    {
        private readonly IUserCheckpointProgressRepository _userCheckpointProgressRepository;

        public UserCheckpointProgressService(IUserCheckpointProgressRepository userCheckpointProgressRepository)
        {
            _userCheckpointProgressRepository = userCheckpointProgressRepository;
        }

        public async Task<UserCheckpointProgressResponse> CreateUserCheckpointProgress(CreateUserCheckpointProgressRequest request)
        {
            if (!ObjectId.TryParse(request.CheckpointAudioId, out _)
                || !ObjectId.TryParse(request.UserTourProgressId, out _)
                )
            {
                throw new FormatException("Invalid format of CheckpointAudioId or UserTourProgressId");
            }

            var userCheckpointProgress = new UserCheckpointProgress
            {
                UserTourProgressId = request.UserTourProgressId,
                CheckpointAudioId = request.CheckpointAudioId,
                TourCheckpointId = request.TourCheckpointId,
                IsCompleted = request.IsCompleted,
                ProgressSeconds = request.ProgressSeconds,
                LastListenedTime = DateTime.UtcNow
            };

            await _userCheckpointProgressRepository.Create(userCheckpointProgress);

            return new UserCheckpointProgressResponse
            {
                Success = true,
                Message = "Tour progress created successfully",
                Response = ModelMapper.MapUserCheckpointProgressToDTO(userCheckpointProgress)
            };
        }

        public async Task DeleteUserCheckpointProgress(string id)
        {
            var progress = await _userCheckpointProgressRepository.FindFirst(t => t.Id == id);
            if (progress == null) throw new KeyNotFoundException("Checkpoint progress not found!");

            await _userCheckpointProgressRepository.Delete(progress);
        }

        public async Task<List<UserCheckpointProgressDTO>> GetAllUserCheckpointProgresss()
        {
            var progresss = await _userCheckpointProgressRepository.GetAll();
            return progresss
                .Select(ModelMapper.MapUserCheckpointProgressToDTO)
                .ToList();
        }

        public async Task<UserCheckpointProgressResponse> GetByTourProgressAndCheckpoint(string tourProgressId, string checkpointId)
        {
            var progress = await _userCheckpointProgressRepository.FindFirst(t => t.UserTourProgressId == tourProgressId && t.TourCheckpointId == checkpointId);
            if (progress == null)
            {
                throw new KeyNotFoundException("Progress not found!");
            }

            return new UserCheckpointProgressResponse
            {
                Success = true,
                Message = "Checkpoint progress retrieved successfully",
                Response = ModelMapper.MapUserCheckpointProgressToDTO(progress)
            };
        }

        public async Task<UserCheckpointProgressResponse> GetUserCheckpointProgressById(string id)
        {
            var progress = await _userCheckpointProgressRepository.FindFirst(t => t.Id == id);
            if (progress == null)
            {
                throw new KeyNotFoundException("Progress not found!");
            }

            return new UserCheckpointProgressResponse
            {
                Success = true,
                Message = "Checkpoint progress retrieved successfully",
                Response = ModelMapper.MapUserCheckpointProgressToDTO(progress)
            };
        }

        public async Task UpdateUserCheckpointProgress(string id, UpdateUserCheckpointProgressRequest request)
        {
            var progress = await _userCheckpointProgressRepository.FindFirst(t => t.Id == id);
            if (progress == null) throw new KeyNotFoundException("Checkpoint Progress Not Found!");
            progress.IsCompleted = request.IsCompleted ?? progress.IsCompleted;
            progress.LastListenedTime = request.LastListenedTime ?? progress.LastListenedTime;
            progress.ProgressSeconds = request.ProgressSeconds ?? progress.ProgressSeconds;

            await _userCheckpointProgressRepository.Update(progress);
        }
    }
}
