using Audivia.Domain.ModelRequests.ChatBot;
using Audivia.Domain.ModelResponses.ChatBot;
using Audivia.Domain.Models.ChatBotHistory;

namespace Audivia.Application.Services.Interface
{
    public interface IChatBotService
    {
        Task<MessageResponse> DetectIntentAsync(MessageRequest request);

        Task<IEnumerable<MessageResponse>> GetChatHistoryAsync(string clientSessionId, int pageNumber, int pageSize);
    }
}
