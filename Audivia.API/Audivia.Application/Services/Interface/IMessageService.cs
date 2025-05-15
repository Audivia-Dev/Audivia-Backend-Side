using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.Message;
using Audivia.Domain.ModelResponses.Message;
using Audivia.Domain.Models;

namespace Audivia.Application.Services.Interface
{
    public interface IMessageService
    {
        Task<MessageResponse> CreateMessage(CreateMessageRequest request);

        Task<MessageListResponse> GetAllMessages();

        Task<MessageResponse> GetMessageById(string id);

        Task<MessageDTO> UpdateMessage(string id, UpdateMessageRequest request);

        Task<MessageDTO> DeleteMessage(string id);
        Task<List<MessageDTO>> GetMessagesByChatRoomId(string chatRoomId);
    }
}
