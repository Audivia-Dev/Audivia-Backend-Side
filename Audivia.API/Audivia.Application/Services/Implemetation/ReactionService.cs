using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.Reaction;
using Audivia.Domain.ModelResponses.Reaction;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class ReactionService : IReactionService
    {
        private readonly IReactionRepository _reactionRepository;

        public ReactionService(IReactionRepository reactionRepository)
        {
            _reactionRepository = reactionRepository;
        }

        public async Task<ReactionResponse> CreateReaction(CreateReactionRequest request)
        {
            if (!ObjectId.TryParse(request.CreatedBy, out _))
            {
                throw new FormatException("Invalid created by value!");
            }
            var reaction = new Reaction
            {
                Type = request.Type,
                PostId = request.PostId,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow,
            };

            await _reactionRepository.Create(reaction);

            return new ReactionResponse
            {
                Success = true,
                Message = "Reaction created successfully",
                Response = ModelMapper.MapReactionToDTO(reaction)
            };
        }

        public async Task<ReactionListResponse> GetAllReactions()
        {
            var reactions = await _reactionRepository.GetAll();
            var reactionDtos = reactions
                .Select(ModelMapper.MapReactionToDTO)
                .ToList();
            return new ReactionListResponse
            {
                Success = true,
                Message = "Reactions retrieved successfully",
                Response = reactionDtos
            };
        }

        public async Task<ReactionResponse> GetReactionById(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid reaction id!");
            }
            var reaction = await _reactionRepository.FindFirst(t => t.Id == id);
            if (reaction == null)
            {
                throw new KeyNotFoundException("Reaction not found!");
            }

            return new ReactionResponse
            {
                Success = true,
                Message = "Reaction retrieved successfully",
                Response = ModelMapper.MapReactionToDTO(reaction)
            };
        }

        public async Task UpdateReaction(string id, UpdateReactionRequest request)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid reaction id!");
            }
            var reaction = await _reactionRepository.FindFirst(t => t.Id == id);
            if (reaction == null) return;

            if (!string.IsNullOrEmpty(request.UpdatedBy) && (!ObjectId.TryParse(request.UpdatedBy, out _) || !request.UpdatedBy.Equals(reaction.CreatedBy)))
            {
                throw new FormatException("Invalid user try to update reaction");
            }
            reaction.Type = request.Type ?? reaction.Type;

            await _reactionRepository.Update(reaction);
        }

        public async Task DeleteReaction(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid reaction id!");
            }
            var reaction = await _reactionRepository.FindFirst(t => t.Id == id);
            if (reaction == null) return;

            await _reactionRepository.Delete(reaction);
        }

    }
}
