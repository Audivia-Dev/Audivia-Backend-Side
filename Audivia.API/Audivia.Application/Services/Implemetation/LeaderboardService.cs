using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.Leaderboard;
using Audivia.Domain.ModelResponses.Leaderboard;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Implemetation
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly ILeaderboardRepository _repository;

        public LeaderboardService(ILeaderboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<LeaderboardResponse> CreateLeaderboardAsync(CreateLeaderboardRequest req)
        {
            var model = new Leaderboard
            {
                TourId = req.TourId,
                UserId = req.UserId,
                Rank = req.Rank,
                Score = req.Score,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _repository.Create(model);

            return new LeaderboardResponse
            {
                Success = true,
                Message = "Created leaderboard successfully!",
                Response = ModelMapper.MapLeaderboardToDTO(model)
            };
        }

        public async Task<LeaderboardResponse> UpdateLeaderboardAsync(string id, UpdateLeaderboardRequest req)
        {
            var model = await _repository.GetById(new ObjectId(id));
            if (model == null)
            {
                return new LeaderboardResponse
                {
                    Success = false,
                    Message = "Leaderboard not found!"
                };
            }

            model.TourId = req.TourId;
            model.UserId = req.UserId;
            model.Rank = req.Rank;
            model.Score = req.Score;
            model.UpdatedAt = DateTime.UtcNow;

            await _repository.Update(model);

            return new LeaderboardResponse
            {
                Success = true,
                Message = "Updated leaderboard successfully!",
                Response = ModelMapper.MapLeaderboardToDTO(model)
            };
        }

        public async Task<LeaderboardResponse> DeleteLeaderboardAsync(string id)
        {
            var model = await _repository.GetById(new ObjectId(id));
            if (model == null)
            {
                return new LeaderboardResponse
                {
                    Success = false,
                    Message = "Leaderboard not found!"
                };
            }

            model.IsDeleted = true;
            await _repository.Update(model);

            return new LeaderboardResponse
            {
                Success = true,
                Message = "Deleted leaderboard successfully!",
                Response = ModelMapper.MapLeaderboardToDTO(model)
            };
        }

        public async Task<LeaderboardListResponse> GetAllLeaderboardsAsync()
        {
            var list = await _repository.GetAll();
            var activeList = list.Where(x => !x.IsDeleted).Select(ModelMapper.MapLeaderboardToDTO).ToList();

            return new LeaderboardListResponse
            {
                Success = true,
                Message = "Fetched all leaderboards successfully!",
                Response = activeList
            };
        }

        public async Task<LeaderboardResponse> GetLeaderboardByIdAsync(string id)
        {
            var model = await _repository.GetById(new ObjectId(id));
            if (model == null)
            {
                return new LeaderboardResponse
                {
                    Success = false,
                    Message = "Leaderboard not found!"
                };
            }

            return new LeaderboardResponse
            {
                Success = true,
                Message = "Fetched leaderboard successfully!",
                Response = ModelMapper.MapLeaderboardToDTO(model)
            };
        }
    }
}

