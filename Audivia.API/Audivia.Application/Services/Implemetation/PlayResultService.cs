using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.PlayResult;
using Audivia.Domain.ModelResponses.PlayResult;
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
    public class PlayResultService : IPlayResultService
    {
        private readonly IPlayResultRepository _playResultRepository;

        public PlayResultService(IPlayResultRepository playResultRepository)
        {
            _playResultRepository = playResultRepository;
        }

        public async Task<PlayResultResponse> CreatePlayResultAsync(CreatePlayResultRequest req)
        {
            if (req == null || string.IsNullOrEmpty(req.SessionId))
            {
                return new PlayResultResponse
                {
                    Success = false,
                    Message = "Invalid request!",
                    Response = null,
                };
            }

            var result = new PlayResult
            {
                SessionId = req.SessionId,
                Score = req.Score ?? 0,
                CompletedAt = DateTime.UtcNow,
            };

            await _playResultRepository.Create(result);

            return new PlayResultResponse
            {
                Success = true,
                Message = "Created PlayResult successfully!",
                Response = ModelMapper.MapPlayResultToDTO(result),
            };
        }

        public async Task<PlayResultResponse> UpdatePlayResultAsync(string id, UpdatePlayResultRequest req)
        {
            var result = await _playResultRepository.GetById(new ObjectId(id));
            if (result == null)
            {
                return new PlayResultResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null
                };
            }

            result.SessionId = req.SessionId;
            result.Score = req.Score ?? result.Score;
            result.CompletedAt = DateTime.UtcNow;   
            await _playResultRepository.Update(result);

            return new PlayResultResponse
            {
                Success = true,
                Message = "Updated successfully!",
                Response = ModelMapper.MapPlayResultToDTO(result)
            };
        }
        public async Task<PlayResultResponse> DeletePlayResultAsync(string id)
        {
            var result = await _playResultRepository.GetById(new ObjectId(id));
            if (result == null)
                return new PlayResultResponse { Success = false, Message = "Not found!" };

            await _playResultRepository.Delete(result);

            return new PlayResultResponse
            {
                Success = true,
                Message = "Deleted successfully!",
                Response = ModelMapper.MapPlayResultToDTO(result),
            };
        }

        public async Task<PlayResultResponse> GetPlayResultByIdAsync(string id)
        {
            var result = await _playResultRepository.GetById(new ObjectId(id));
            return result == null
                ? new PlayResultResponse { Success = false, Message = "Not found!" }
                : new PlayResultResponse
                {
                    Success = true,
                    Message = "Fetched successfully!",
                    Response = ModelMapper.MapPlayResultToDTO(result),
                };
        }

        public async Task<PlayResultListResponse> GetAllPlayResultsAsync()
        {
            var list = await _playResultRepository.GetAll();
            return new PlayResultListResponse
            {
                Success = true,
                Message = "Fetched all successfully!",
                Response = list.Select(ModelMapper.MapPlayResultToDTO).ToList()
            };
        }
    }
}
