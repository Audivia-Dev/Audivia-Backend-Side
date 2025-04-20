using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
namespace Audivia.Domain.ModelResponses.Message
{
    public class MessageListResponse : AbstractApiResponse<List<MessageDTO>>
    {
        public override List<MessageDTO> Response { get; set; }
    }
}
