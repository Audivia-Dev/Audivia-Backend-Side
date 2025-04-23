using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.PlaySession;
using Audivia.Domain.ModelResponses.PlaySession;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class PlaySessionService : IPlaySessionService
    {
        private readonly IPlaySessionRepository _playSessionRepository;

        public PlaySessionService(IPlaySessionRepository playSessionRepository)
        {
            _playSessionRepository = playSessionRepository;
        }

        public async Task<PlaySessionResponse> CreatePlaySessionAsync(CreatePlaySessionRequest req)
        {
            if (req == null || string.IsNullOrEmpty(req.UserId))
            {
                return new PlaySessionResponse { Success = false, Message = "Invalid request!" };
            }

            var session = new PlaySession
            {
                UserId = req.UserId,
                TourId = req.TourId,
                GroupId = req.GroupId,
                StartTime = req.StartTime ?? DateTime.UtcNow,
                EndTime = req.EndTime,
            };

            await _playSessionRepository.Create(session);

            return new PlaySessionResponse
            {
                Success = true,
                Message = "Created successfully!",
                Response = ModelMapper.MapPlaySessionToDTO(session),
            };
        }

        public async Task<PlaySessionResponse> UpdatePlaySessionAsync(string id, UpdatePlaySessionRequest req)
        {
            var session = await _playSessionRepository.GetById(new ObjectId(id));
            if (session == null)
            {
                return new PlaySessionResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null
                };
            }

            session.UserId = req.UserId ?? session.UserId;
            session.GroupId = req.GroupId ?? session.GroupId;
            session.TourId = req.TourId ?? session.TourId;
            session.StartTime = req.StartTime ?? session.StartTime;
            session.EndTime = req.EndTime;

            await _playSessionRepository.Update(session);

            return new PlaySessionResponse
            {
                Success = true,
                Message = "Updated successfully!",
                Response = ModelMapper.MapPlaySessionToDTO(session)
            };
        }
        public async Task<PlaySessionResponse> DeletePlaySessionAsync(string id)
        {
            var session = await _playSessionRepository.GetById(new ObjectId(id));
            if (session == null)
                return new PlaySessionResponse { Success = false, Message = "Not found!" };

            await _playSessionRepository.Delete(session);

            return new PlaySessionResponse
            {
                Success = true,
                Message = "Deleted successfully!",
                Response = ModelMapper.MapPlaySessionToDTO(session),
            };
        }

        public async Task<PlaySessionResponse> GetPlaySessionByIdAsync(string id)
        {
            var session = await _playSessionRepository.GetById(new ObjectId(id));
            return session == null
                ? new PlaySessionResponse { Success = false, Message = "Not found!" }
                : new PlaySessionResponse
                {
                    Success = true,
                    Message = "Fetched successfully!",
                    Response = ModelMapper.MapPlaySessionToDTO(session),
                };
        }

        public async Task<PlaySessionListResponse> GetAllPlaySessionsAsync()
        {
            var list = await _playSessionRepository.GetAll();
            return new PlaySessionListResponse
            {
                Success = true,
                Message = "Fetched all successfully!",
                Response = list.Select(ModelMapper.MapPlaySessionToDTO).ToList()
            };
        }
    }
}
