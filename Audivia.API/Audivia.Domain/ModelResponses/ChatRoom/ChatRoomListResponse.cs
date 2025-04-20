using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.ChatRoom
{
    public class ChatRoomListResponse : AbstractApiResponse<List<ChatRoomDTO>>
    {
        public override List<ChatRoomDTO> Response { get; set; }
    }
}
