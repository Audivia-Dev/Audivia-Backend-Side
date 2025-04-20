namespace Audivia.Domain.ModelRequests.ChatRoomMember
{
    public class CreateChatRoomMemberRequest
    {
        public string ChatRoomId { get; set; }
        public string UserId { get; set; }
        public string Nickname { get; set; }
        public bool IsHost { get; set; }
    }
}
