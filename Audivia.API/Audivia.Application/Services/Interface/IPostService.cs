using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.Post;
using Audivia.Domain.ModelResponses.Post;

namespace Audivia.Application.Services.Interface
{
    public interface IPostService
    {
        Task<PostResponse> CreatePost(CreatePostRequest request);

        Task<PostListResponse> GetAllPosts();

        Task<PostResponse> GetPostById(string id);

        Task UpdatePost(string id, UpdatePostRequest request);

        Task DeletePost(string id);
    }
}
