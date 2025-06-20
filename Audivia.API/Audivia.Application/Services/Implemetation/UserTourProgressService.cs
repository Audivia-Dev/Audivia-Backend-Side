using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.UserTourProgress;
using Audivia.Domain.ModelResponses.UserTourProgress;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class UserTourProgressService : IUserTourProgressService
    {
        private readonly IUserTourProgressRepository _userTourProgressRepository;
        private readonly IUserCheckpointProgressRepository _userCheckpointProgressRepository;

        public UserTourProgressService(IUserTourProgressRepository userTourProgressRepository, IUserCheckpointProgressRepository userCheckpointProgressRepository)
        {
            _userTourProgressRepository = userTourProgressRepository;
            _userCheckpointProgressRepository = userCheckpointProgressRepository;
        }

        public async Task<UserTourProgressResponse> CreateUserTourProgress(CreateUserTourProgressRequest request)
        {
            if (!ObjectId.TryParse(request.UserId, out _) 
                || !ObjectId.TryParse(request.TourId, out _)
                || !ObjectId.TryParse(request.CurrentCheckpointId, out _)
                )
            {
                throw new FormatException("Invalid format of UserId or TourId or TourCheckpointId");
            }
            var userTourProgress = new UserTourProgress
            {
                TourId = request.TourId,
                UserId = request.UserId,
                StartedAt = request.StartedAt,
                FinishedAt = request.FinishedAt,
                IsCompleted = request.IsCompleted,
                CurrentCheckpointId = request.CurrentCheckpointId,
                GroupId = request.GroupId,
                Score = request.Score,
                GroupMode = request.GroupMode
            };

            await _userTourProgressRepository.Create(userTourProgress);

            return new UserTourProgressResponse
            {
                Success = true,
                Message = "Tour progress created successfully",
                Response = ModelMapper.MapUserTourProgressToDTO(userTourProgress)
            };
        }

        public async Task<List<UserTourProgressDTO>> GetAllUserTourProgresss()
        {
            var progresss = await _userTourProgressRepository.GetAll();
            return progresss
                .Select(ModelMapper.MapUserTourProgressToDTO)
                .ToList();
        }

        public async Task<UserTourProgressResponse> GetUserTourProgressById(string id)
        {
            var progress = await _userTourProgressRepository.FindFirst(t => t.Id == id);
            if (progress == null)
            {
                return new UserTourProgressResponse { Success = true, Message = "Tour progress not existed!", Response = null };
            }

            var checkpointProgress = await _userCheckpointProgressRepository.GetByTourProgressId(progress.Id);

            return new UserTourProgressResponse
            {
                Success = true,
                Message = "Tour progress retrieved successfully",
                Response = ModelMapper.MapUserTourProgressToDTO(progress, checkpointProgress)
            };
        }

        public async Task<UserTourProgressResponse> GetUserTourProgressByUserIdAndTourId(string userId, string tourId)
        {
            var progress = await _userTourProgressRepository.GetByUserIdAndTourId(userId, tourId);
            if (progress == null)
            {
                return new UserTourProgressResponse { Success = true, Message = "Tour progress not existed!", Response = null };
            }
            var checkpointProgress = await _userCheckpointProgressRepository.GetByTourProgressId(progress.Id);

            return new UserTourProgressResponse
            {
                Success = true,
                Message = "Tour progress retrieved successfully",
                Response = ModelMapper.MapUserTourProgressToDTO(progress, checkpointProgress)
            };
        }

        public async Task UpdateUserTourProgress(string id, UpdateUserTourProgressRequest request)
        {
            var progress = await _userTourProgressRepository.FindFirst(t => t.Id == id);
            if (progress == null) throw new KeyNotFoundException("Progress Not Found!");
            progress.StartedAt = request.StartedAt ?? progress.StartedAt;
            progress.FinishedAt = request.FinishedAt ?? progress.FinishedAt;
            progress.IsCompleted = request.IsCompleted ?? progress.IsCompleted;
            progress.CurrentCheckpointId = request.CurrentCheckpointId ?? progress.CurrentCheckpointId;
            progress.Score = request.Score ?? progress.Score;
            progress.GroupMode = request.GroupMode ?? progress.GroupMode;
            progress.GroupId = request.GroupId ?? progress.GroupId;

            await _userTourProgressRepository.Update(progress);
        }

        public async Task DeleteUserTourProgress(string id)
        {
            var progress = await _userTourProgressRepository.FindFirst(t => t.Id == id);
            if (progress == null) throw new KeyNotFoundException("Progress not found!");

            await _userTourProgressRepository.Delete(progress);
        }

    }
}
