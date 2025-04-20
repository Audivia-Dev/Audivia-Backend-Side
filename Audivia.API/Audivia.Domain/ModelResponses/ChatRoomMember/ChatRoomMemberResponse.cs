using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
namespace Audivia.Domain.ModelResponses.ChatRoomMember
{
    public class ChatRoomMemberResponse : AbstractApiResponse<ChatRoomMemberDTO>
    {
        public override ChatRoomMemberDTO Response { get; set; }
    }
}
