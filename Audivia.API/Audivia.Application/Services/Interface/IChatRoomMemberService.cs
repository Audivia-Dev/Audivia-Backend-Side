using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.ChatRoomMember;
using Audivia.Domain.ModelResponses.ChatRoomMember;

namespace Audivia.Application.Services.Interface
{
    public interface IChatRoomMemberService
    {
        Task<ChatRoomMemberResponse> CreateChatRoomMember(CreateChatRoomMemberRequest request);

        Task<ChatRoomMemberListResponse> GetAllChatRoomMembers();

        Task<ChatRoomMemberResponse> GetChatRoomMemberById(string id);

        Task UpdateChatRoomMember(string id, UpdateChatRoomMemberRequest request);

        Task DeleteChatRoomMember(string id);
        Task<ChatRoomMemberDTO> GetMemberById(string id);
    }
}
