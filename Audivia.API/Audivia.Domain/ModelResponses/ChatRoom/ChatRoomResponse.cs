using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.ChatRoom
{
    public class ChatRoomResponse : AbstractApiResponse<ChatRoomDTO>
    {
        public override ChatRoomDTO Response { get; set; }
    }
}
