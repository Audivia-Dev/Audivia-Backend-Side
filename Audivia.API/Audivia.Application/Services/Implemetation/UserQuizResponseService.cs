using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.UserQuizResponse;
using Audivia.Domain.ModelResponses.UserQuizResponse;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class UserQuizResponseService : IUserQuizResponseService
    {
        private readonly IUserQuizResponseRepository _userQuizResponseRepository;

        public UserQuizResponseService(IUserQuizResponseRepository userQuizResponseRepository)
        {
            _userQuizResponseRepository = userQuizResponseRepository;
        }

        public async Task<UserQuizResponseResponse> CreateUserQuizResponseAsync(CreateUserQuizResponseRequest req)
        {
            UserQuizResponse newQuizResponse = new UserQuizResponse { UserId = req.UserId, QuizId = req.QuizId, IsDone = false, CorrectAnswersCount = 0, RespondedAt = DateTime.UtcNow };
            await _userQuizResponseRepository.Create(newQuizResponse);
            return new UserQuizResponseResponse
            {
                Success = true,
                Message = "Created successfully!",
                Response = ModelMapper.MapUserQuizResponseToDTO(newQuizResponse),
            };
        }

        public async Task<UserQuizResponseResponse> DeleteUserQuizResponseAsync(string id)
        {
            var userQuizResponse = await _userQuizResponseRepository.GetById(new ObjectId(id.ToString()));
            if (userQuizResponse == null)
            {
                return new UserQuizResponseResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null,
                };
            }
            await _userQuizResponseRepository.Delete(userQuizResponse);
            return new UserQuizResponseResponse
            {
                Success = true,
                Message = "Deleted successfully!",
                Response = ModelMapper.MapUserQuizResponseToDTO(userQuizResponse),
            };

        }

        public async Task<UserQuizResponseListResponse> GetAllUserQuizResponseAsync()
        {
            var list = await _userQuizResponseRepository.GetAll();
            var rs = list.Select(ModelMapper.MapUserQuizResponseToDTO).ToList();
            return new UserQuizResponseListResponse
            {
                Success = true,
                Message = "Fetched all user responses successfully!",
                Response = rs
            };
        }

        public async Task<UserQuizResponseResponse> GetUserQuizResponseByIdAsync(string id)
        {
            var userQuizResponse = await _userQuizResponseRepository.GetById(new ObjectId(id.ToString()));
            if (userQuizResponse == null)
            {
                return new UserQuizResponseResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null,
                };
            }
            return new UserQuizResponseResponse
            {
                Success = true,
                Message = "Fetch User Quiz Response successfully!",
                Response = ModelMapper.MapUserQuizResponseToDTO(userQuizResponse),
            };
        }

        public async Task<UserQuizResponseResponse> GetUserQuizResponseByQuizIdAndUserIdAsync(string quizId, string userId)
        {
            var userQuizResponse = await _userQuizResponseRepository.FindFirst(q => q.QuizId == quizId && q.UserId == userId);
            if (userQuizResponse == null)
            {
                return new UserQuizResponseResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null,
                };
            }
            return new UserQuizResponseResponse
            {
                Success = true,
                Message = "Fetch User Quiz Response successfully!",
                Response = ModelMapper.MapUserQuizResponseToDTO(userQuizResponse),
            };
        }

        public async Task<UserQuizResponseResponse> UpdateUserQuizResponseAsync(string id, UpdateUserQuizResponseRequest req)
        {
            var userQuizResponse = await _userQuizResponseRepository.GetById(new ObjectId(id.ToString()));
            if (userQuizResponse == null)
            {
                return new UserQuizResponseResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null,
                };
            }
            userQuizResponse.QuizId = req.QuizId ?? userQuizResponse.QuizId;
            userQuizResponse.UserId = req.UserId ?? userQuizResponse.UserId;
            userQuizResponse.CorrectAnswersCount = req.CorrectAnswersCount ?? userQuizResponse.CorrectAnswersCount;
            userQuizResponse.IsDone = req.IsDone ?? userQuizResponse.IsDone;
            userQuizResponse.RespondedAt = DateTime.UtcNow;
            await _userQuizResponseRepository.Update(userQuizResponse);
            
            return new UserQuizResponseResponse
            {
                Success = true,
                Message = "Updated successfully!",
                Response = ModelMapper.MapUserQuizResponseToDTO(userQuizResponse),
            };
        }
    }
}
