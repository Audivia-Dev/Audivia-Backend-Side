using Audivia.Domain.Models.ChatBotHistory;

namespace Audivia.Domain.ModelResponses.ChatBot
{
    public class MessageResponse
    {
        public string Reply { get; set; }
        public string Timestamp { get; set; }
        public SenderType Sender { get; set; }
    }
}
