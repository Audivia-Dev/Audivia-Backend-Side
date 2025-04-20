using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.ModelRequests.Comment;
using Audivia.Domain.ModelResponses.Comment;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentResponse> CreateComment(CreateCommentRequest request)
        {
            if (!ObjectId.TryParse(request.CreatedBy, out _))
            {
                return new CommentResponse
                {
                    Success = false,
                    Message = "Invalid CreatedBy format",
                    Response = null
                };
            }
            var comment = new Comment
            {
                Content = request.Content,
                PostId = request.PostId,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _commentRepository.Create(comment);

            return new CommentResponse
            {
                Success = true,
                Message = "Comment created successfully",
                Response = ModelMapper.MapCommentToDTO(comment)
            };
        }

        public async Task<CommentListResponse> GetAllComments()
        {
            var comments = await _commentRepository.GetAll();
            var commentDtos = comments
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapCommentToDTO)
                .ToList();
            return new CommentListResponse
            {
                Success = true,
                Message = "Comments retrieved successfully",
                Response = commentDtos
            };
        }

        public async Task<CommentResponse> GetCommentById(string id)
        {
            var comment = await _commentRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (comment == null)
            {
                return new CommentResponse
                {
                    Success = false,
                    Message = "Comment not found",
                    Response = null
                };
            }

            return new CommentResponse
            {
                Success = true,
                Message = "Comment retrieved successfully",
                Response = ModelMapper.MapCommentToDTO(comment)
            };
        }

        public async Task UpdateComment(string id, UpdateCommentRequest request)
        {
            var comment = await _commentRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (comment == null) return;

            if (!string.IsNullOrEmpty(request.UpdatedBy) && (!ObjectId.TryParse(request.UpdatedBy, out _) || !request.UpdatedBy.Equals(comment.CreatedBy)))
            {
                throw new FormatException("Invalid user try to update comment");
            }
            comment.Content = request.Content ?? comment.Content;
            comment.UpdatedAt = DateTime.UtcNow;

            await _commentRepository.Update(comment);
        }

        public async Task DeleteComment(string id)
        {
            var comment = await _commentRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (comment == null) return;

            comment.IsDeleted = true;
            comment.UpdatedAt = DateTime.UtcNow;

            await _commentRepository.Update(comment);
        }
    }
}
