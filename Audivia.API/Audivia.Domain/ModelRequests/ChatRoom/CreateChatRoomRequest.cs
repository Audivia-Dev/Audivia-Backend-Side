namespace Audivia.Domain.ModelRequests.ChatRoom
{
    public class CreateChatRoomRequest
    {
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public string Type { get; set; }
    }
}
