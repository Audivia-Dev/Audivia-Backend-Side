using Audivia.Application.Services.Interface;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.Post;
using Audivia.Domain.ModelResponses.Post;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PostResponse> CreatePost(CreatePostRequest request)
        {
            if (!ObjectId.TryParse(request.CreatedBy, out _))
            {
                return new PostResponse
                {
                    Success = false,
                    Message = "Invalid CreatedBy format",
                    Response = null
                };
            }
            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _postRepository.Create(post);

            return new PostResponse
            {
                Success = true,
                Message = "Post created successfully",
                Response = ModelMapper.MapPostToDTO(post)
            };
        }

        public async Task<PostListResponse> GetAllPosts()
        {
            var posts = await _postRepository.GetAll();
            var postDtos = posts
                .Where(t => !t.IsDeleted)
                .Select(ModelMapper.MapPostToDTO)
                .ToList();
            return new PostListResponse
            {
                Success = true,
                Message = "Posts retrieved successfully",
                Response = postDtos
            };
        }

        public async Task<PostResponse> GetPostById(string id)
        {
            var post = await _postRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (post == null)
            {
                return new PostResponse
                {
                    Success = false,
                    Message = "Post not found",
                    Response = null
                };
            }

            return new PostResponse
            {
                Success = true,
                Message = "Post retrieved successfully",
                Response = ModelMapper.MapPostToDTO(post)
            };
        }

        public async Task UpdatePost(string id, UpdatePostRequest request)
        {
            var post = await _postRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (post == null) return;

            if (!string.IsNullOrEmpty(request.UpdatedBy) && (!ObjectId.TryParse(request.UpdatedBy, out _) || !request.UpdatedBy.Equals(post.CreatedBy)))
            {
                throw new FormatException("Invalid user try to update post");
            }
            post.Title = request.Title ?? post.Title;
            post.Content = request.Content ?? post.Content;
            post.ImageUrl = request.ImageUrl ?? post.ImageUrl;
            post.UpdatedAt = DateTime.UtcNow;

            await _postRepository.Update(post);
        }

        public async Task DeletePost(string id)
        {
            var post = await _postRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (post == null) return;

            post.IsDeleted = true;
            post.UpdatedAt = DateTime.UtcNow;

            await _postRepository.Update(post);
        }
    }
}
