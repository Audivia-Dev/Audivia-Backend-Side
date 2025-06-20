using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.Comment;
using Audivia.Domain.ModelResponses.Comment;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Audivia.Application.Services.Implemetation
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        public async Task<CommentResponse> CreateComment(CreateCommentRequest request)
        {
            if (!ObjectId.TryParse(request.CreatedBy, out _))
            {
                throw new FormatException("Invalid created by value!");
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
            comments = comments.Where(t => !t.IsDeleted);

            List<CommentDTO> commentDtos = new List<CommentDTO>();
            foreach (var comment in comments)
            {
                var commentDto = ModelMapper.MapCommentToDTO(comment);
                commentDtos.Add(commentDto);
            }
            return new CommentListResponse
            {
                Success = true,
                Message = "Comments retrieved successfully",
                Response = commentDtos
            };
        }

        public async Task<CommentResponse> GetCommentById(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid comment id!");
            }
            var comment = await _commentRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (comment == null)
            {
                throw new KeyNotFoundException("Comment not found!");
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
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid comment id!");
            }
            var comment = await _commentRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (comment == null)
            {
                throw new KeyNotFoundException("Comment not found!");
            }

            if (!string.IsNullOrEmpty(request.UpdatedBy) && (!ObjectId.TryParse(request.UpdatedBy, out _) || !request.UpdatedBy.Equals(comment.CreatedBy)))
            {
                throw new FormatException("Invalid user try to update comment");
            }
            comment.Content = request.Content ?? comment.Content;
            comment.UpdatedAt = DateTime.UtcNow;

            await _commentRepository.Update(comment);
        }

        public async Task DeleteComment(string id, string userId)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid comment id!");
            }
            var comment = await _commentRepository.FindFirst(t => t.Id == id);
            if (comment == null) return;
            if (comment.CreatedBy != userId)
            {
                throw new HttpRequestException("You are not allowed to delete this comment!");
            }
            await _commentRepository.Delete(comment);
        }

        public async Task<CommentListResponse> GetByPost(string postId)
        {
            FilterDefinition<Comment> filter = Builders<Comment>.Filter.Eq(c => c.PostId, postId);
            var comments = await _commentRepository.Search(filter);
            
            comments = comments.Where(t => !t.IsDeleted);

            List<CommentDTO> commentDtos = new List<CommentDTO>();
            foreach (var comment in comments)
            {
                var user = await _userRepository.FindFirst(u => u.Id == comment.CreatedBy);
                var commentDto = ModelMapper.MapCommentToDTO(comment, user.Username);
                commentDtos.Add(commentDto);
            }
            return new CommentListResponse
            {
                Success = true,
                Message = "Comments retrieved successfully",
                Response = commentDtos
            };
        }
    }
}
