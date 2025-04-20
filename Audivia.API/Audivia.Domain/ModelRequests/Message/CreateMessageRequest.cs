namespace Audivia.Domain.ModelRequests.Message
{
    public class CreateMessageRequest
    {
        public string Content { get; set; }
        public string Type { get; set; } // enum
        public string Status { get; set; } // enum
        public string SenderId { get; set; }
        public string ChatRoomId { get; set; }
    }
}
