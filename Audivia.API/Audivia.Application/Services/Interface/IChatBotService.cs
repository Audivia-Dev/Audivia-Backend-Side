
using Audivia.Domain.ModelRequests.ChatBot;
using Audivia.Domain.ModelResponses.ChatBot;

namespace Audivia.Application.Services.Interface
{
    public interface IChatBotService
    {
        Task<MessageResponse> DetectIntentAsync(MessageRequest request);
    }
}
