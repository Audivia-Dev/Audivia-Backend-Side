namespace Audivia.Domain.ModelRequests.Message
{
    public class UpdateMessageRequest
    {
        public string Content { get; set; }
        public string Type { get; set; } // enum
        public string Status { get; set; } // enum
    }
}
