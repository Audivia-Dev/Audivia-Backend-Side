using Audivia.Domain.ModelRequests.Message;
using Audivia.Domain.ModelResponses.Message;

namespace Audivia.Application.Services.Interface
{
    public interface IMessageService
    {
        Task<MessageResponse> CreateMessage(CreateMessageRequest request);

        Task<MessageListResponse> GetAllMessages();

        Task<MessageResponse> GetMessageById(string id);

        Task UpdateMessage(string id, UpdateMessageRequest request);

        Task DeleteMessage(string id);
    }
}
