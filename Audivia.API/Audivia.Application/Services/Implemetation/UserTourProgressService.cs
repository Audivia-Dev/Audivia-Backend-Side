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

        public UserTourProgressService(IUserTourProgressRepository userTourProgressRepository)
        {
            _userTourProgressRepository = userTourProgressRepository;
        }

        public async Task<UserTourProgressResponse> CreateUserTourProgress(CreateUserTourProgressRequest request)
        {
            if (!ObjectId.TryParse(request.UserId, out _) 
                || !ObjectId.TryParse(request.TourId, out _)
                || !ObjectId.TryParse(request.CurrentCheckpointId, out _)
                )
            {
                return new UserTourProgressResponse
                {
                    Success = false,
                    Message = "Invalid format of UserId or TourId or CheckpointId ",
                    Response = null
                };
            }
            var userTourProgress = new UserTourProgress
            {
                TourId = request.TourId,
                UserId = request.UserId,
                StartedAt = request.StartedAt,
                FinishedAt = request.FinishedAt,
                Status = request.Status,
                CurrentCheckpointId = request.CurrentCheckpointId,
                GroupId = request.GroupId,
                Score = request.Score,
                GroupMode = request.GroupMode,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
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
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapUserTourProgressToDTO)
                .ToList();
        }

        public async Task<UserTourProgressResponse> GetUserTourProgressById(string id)
        {
            var progress = await _userTourProgressRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (progress == null)
            {
                return new UserTourProgressResponse
                {
                    Success = false,
                    Message = "Tour progress not found",
                    Response = null
                };
            }

            return new UserTourProgressResponse
            {
                Success = true,
                Message = "Tour progress retrieved successfully",
                Response = ModelMapper.MapUserTourProgressToDTO(progress)
            };
        }

        public async Task UpdateUserTourProgress(string id, UpdateUserTourProgressRequest request)
        {
            var progress = await _userTourProgressRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (progress == null) return;
            progress.StartedAt = request.StartedAt ?? progress.StartedAt;
            progress.FinishedAt = request.FinishedAt ?? progress.FinishedAt;
            progress.Status = request.Status ?? progress.Status;
            progress.CurrentCheckpointId = request.CurrentCheckpointId ?? progress.CurrentCheckpointId;
            progress.Score = request.Score ?? progress.Score;
            progress.GroupMode = request.GroupMode ?? progress.GroupMode;
            progress.GroupId = request.GroupId ?? progress.GroupId;
            progress.UpdatedAt = DateTime.UtcNow;

            await _userTourProgressRepository.Update(progress);
        }

        public async Task DeleteUserTourProgress(string id)
        {
            var progress = await _userTourProgressRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (progress == null) return;

            progress.IsDeleted = true;
            progress.UpdatedAt = DateTime.UtcNow;

            await _userTourProgressRepository.Update(progress);
        }

    }
}
