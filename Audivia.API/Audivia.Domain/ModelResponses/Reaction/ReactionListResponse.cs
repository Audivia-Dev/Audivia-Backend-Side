using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.Reaction
{
    public class ReactionListResponse : AbstractApiResponse<List<ReactionDTO>>
    {
        public override List<ReactionDTO> Response { get; set; }
    }
}
