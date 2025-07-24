using Audivia.Domain.ModelRequests.UserQuestionResponse;
using Audivia.Domain.ModelResponses.UserQuestionResponse;

namespace Audivia.Application.Services.Interface
{
    public interface IUserQuestionResponseService
    {
        Task<UserQuestionResponseResponse> CreateUserQuestionResponseAsync(CreateUserQuestionResponseRequest req);
    }
}
