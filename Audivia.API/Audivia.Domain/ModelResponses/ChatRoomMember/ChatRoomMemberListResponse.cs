using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.ChatRoomMember
{
    public class ChatRoomMemberListResponse : AbstractApiResponse<List<ChatRoomMemberDTO>>
    {
        public override List<ChatRoomMemberDTO> Response { get; set; }
    }
}
