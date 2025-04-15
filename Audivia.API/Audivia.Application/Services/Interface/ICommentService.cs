using Audivia.Domain.ModelRequests.Comment;
using Audivia.Domain.ModelResponses.Comment;

namespace Audivia.Application.Services.Interface
{
    public interface ICommentService
    {
        Task<CommentResponse> CreateComment(CreateCommentRequest request);

        Task<CommentListResponse> GetAllComments();

        Task<CommentResponse> GetCommentById(string id);

        Task UpdateComment(string id, UpdateCommentRequest request);

        Task DeleteComment(string id);
    }
}
