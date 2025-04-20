using Audivia.Domain.Enums;

namespace Audivia.Domain.ModelRequests.Reaction
{
    public class CreateReactionRequest
    {
        public ReactionType Type { get; set; }
        public string? PostId { get; set; }
        public string? CreatedBy { get; set; }
    }
}
