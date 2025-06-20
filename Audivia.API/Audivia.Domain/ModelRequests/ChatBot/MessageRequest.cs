namespace Audivia.Domain.ModelRequests.ChatBot
{
    public class MessageRequest
    {
        public required string Text { get; set; }
        public required string UserId { get; set; }
        public required string ClientSessionId { get; set; }
    }
}
