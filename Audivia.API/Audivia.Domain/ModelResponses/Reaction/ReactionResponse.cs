using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.Reaction
{
    public class ReactionResponse : AbstractApiResponse<ReactionDTO>
    {
        public override ReactionDTO Response { get; set; }
    }
}
