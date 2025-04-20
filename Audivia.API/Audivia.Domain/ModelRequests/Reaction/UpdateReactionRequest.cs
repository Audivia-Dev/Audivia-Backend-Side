using Audivia.Domain.Enums;

namespace Audivia.Domain.ModelRequests.Reaction
{
    public class UpdateReactionRequest
    {
        public ReactionType? Type { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
