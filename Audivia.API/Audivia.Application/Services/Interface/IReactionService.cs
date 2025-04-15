using Audivia.Domain.ModelRequests.Reaction;
using Audivia.Domain.ModelResponses.Reaction;

namespace Audivia.Application.Services.Interface
{
    public interface IReactionService
    {
        Task<ReactionResponse> CreateReaction(CreateReactionRequest request);

        Task<ReactionListResponse> GetAllReactions();

        Task<ReactionResponse> GetReactionById(string id);

        Task UpdateReaction(string id, UpdateReactionRequest request);

        Task DeleteReaction(string id);
    }
}
