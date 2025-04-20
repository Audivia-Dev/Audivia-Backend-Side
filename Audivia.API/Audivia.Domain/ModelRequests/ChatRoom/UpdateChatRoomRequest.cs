namespace Audivia.Domain.ModelRequests.ChatRoom
{
    public class UpdateChatRoomRequest
    {
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
        public string? Type { get; set; }
    }
}
