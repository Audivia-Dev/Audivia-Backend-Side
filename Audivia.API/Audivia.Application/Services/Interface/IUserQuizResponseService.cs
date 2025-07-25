using Audivia.Domain.ModelRequests.UserQuizResponse;
using Audivia.Domain.ModelResponses.UserQuizResponse;

namespace Audivia.Application.Services.Interface
{
    public interface IUserQuizResponseService
    {
        Task<UserQuizResponseResponse> CreateUserQuizResponseAsync(CreateUserQuizResponseRequest req);
        Task<UserQuizResponseResponse> UpdateUserQuizResponseAsync(string id, UpdateUserQuizResponseRequest req);
        Task<UserQuizResponseResponse> DeleteUserQuizResponseAsync(string id);
        Task<UserQuizResponseListResponse> GetAllUserQuizResponseAsync();
        Task<UserQuizResponseResponse> GetUserQuizResponseByIdAsync(string id);
        Task<UserQuizResponseResponse> GetUserQuizResponseByQuizIdAndUserIdAsync(string quizId, string userId);
    }
}
