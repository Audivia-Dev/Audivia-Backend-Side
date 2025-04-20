using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.Message
{
    public class MessageResponse : AbstractApiResponse<MessageDTO>
    {
        public override MessageDTO Response { get; set; }
    }
}
