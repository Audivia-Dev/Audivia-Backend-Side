using Audivia.Domain.ModelRequests.ChatRoom;
using Audivia.Domain.ModelResponses.ChatRoom;
using Audivia.Domain.Models;

namespace Audivia.Application.Services.Interface
{
    public interface IChatRoomService
    {
        Task<ChatRoomResponse> CreateChatRoom(CreateChatRoomRequest request);

        Task<ChatRoomListResponse> GetAllChatRooms();

        Task<ChatRoomResponse> GetChatRoomById(string id);

        Task UpdateChatRoom(string id, UpdateChatRoomRequest request);

        Task DeleteChatRoom(string id);
    }
}
