using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.UserResponse;
using Audivia.Domain.ModelResponses.UserResponse;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class UserResponseService : IUserResponseService
    {
        private readonly IUserResponseRepository _userResponseRepository;
        public UserResponseService(IUserResponseRepository userResponseRepository)
        {
            _userResponseRepository = userResponseRepository;
        }
        public async Task<UserResponseResponse> CreateUserResponseAsync(CreateUserResponseRequest req)
        {
            if (req is null)
            {
                return new UserResponseResponse
                {
                    Success = false,
                    Message = "Created UserResponse failed!",
                    Response = null,

                };
            }
            var userResponse = new Domain.Models.UserResponse
            {
                UserId = req.UserId,
                AnswerId = req.AnswerId,
                QuestionId = req.QuestionId,
                RespondedAt = DateTime.UtcNow,
            };

            await _userResponseRepository.Create(userResponse);
            return new UserResponseResponse
            {
                Success = true,
                Message = "Created user response successfully!",
                Response = ModelMapper.MapUserResponseToDTO(userResponse),

            };
        }

        public async Task<UserResponseResponse> DeleteUserResponseAsync(string id)
        {
            var userResponse = await _userResponseRepository.GetById(new ObjectId(id.ToString()));
            if (userResponse == null)
            {
                return new UserResponseResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null,
                };
            }
            await _userResponseRepository.Delete(userResponse);
            return new UserResponseResponse
            {
                Success = true,
                Message = "Deleted successfully!",
                Response = ModelMapper.MapUserResponseToDTO(userResponse),
            };

        }

        public async Task<UserResponseListResponse> GetAllUserResponseAsync()
        {
            var list = await _userResponseRepository.GetAll();
            var rs = list.Select(ModelMapper.MapUserResponseToDTO).ToList();
            return new UserResponseListResponse
            {
                Success = true,
                Message = "Fetched all user responses successfully!",
                Response = rs
            };
        }

        public async Task<UserResponseResponse> GetUserResponseByIdAsync(string id)
        {
            var userResponse = await _userResponseRepository.GetById(new ObjectId(id.ToString()));
            if (userResponse == null)
            {
                return new UserResponseResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null,
                };
            }
            return new UserResponseResponse
            {
                Success = true,
                Message = "Fetch UsereResponse successfully!",
                Response = ModelMapper.MapUserResponseToDTO(userResponse),
            };
        }

        public async Task<UserResponseResponse> UpdateUserResponseAsync(string id, UpdateUserResponseRequest req)
        {
            var userResponse = await _userResponseRepository.GetById(new ObjectId(id.ToString()));
            if (userResponse == null)
            {
                return new UserResponseResponse
                {
                    Success = false,
                    Message = "Not found!",
                    Response = null,
                };
            }
            userResponse.AnswerId = req.AnswerId ?? userResponse.AnswerId;
            userResponse.UserId = req.UserId ?? userResponse.UserId;
            userResponse.QuestionId = req.QuestionId ?? userResponse.QuestionId;
            userResponse.RespondedAt = DateTime.UtcNow;
            await _userResponseRepository.Update(userResponse);
            return new UserResponseResponse
            {
                Success = true,
                Message = "Updated successfully!",
                Response = ModelMapper.MapUserResponseToDTO(userResponse),
            };
        }
    }
}
