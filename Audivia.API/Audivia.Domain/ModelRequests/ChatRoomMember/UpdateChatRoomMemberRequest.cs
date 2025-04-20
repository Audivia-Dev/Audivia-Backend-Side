namespace Audivia.Domain.ModelRequests.ChatRoomMember
{
    public class UpdateChatRoomMemberRequest
    {
        public string? Nickname { get; set; }
        public bool? IsHost { get; set; }
    }
}
