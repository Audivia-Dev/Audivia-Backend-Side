using Audivia.Application.Services.Interface;
using Audivia.Application.Utils.Helper;
using Audivia.Domain.Commons.Mapper;
using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.Post;
using Audivia.Domain.ModelResponses.Post;
using Audivia.Domain.Models;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Audivia.Application.Services.Implemetation
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IReactionRepository _reactionRepository;
        private readonly ICommentRepository _commentRepository;

        public PostService(IPostRepository postRepository, IUserRepository userRepository, IReactionRepository reactionRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _reactionRepository = reactionRepository;
            _commentRepository = commentRepository;
        }

        public async Task<PostResponse> CreatePost(CreatePostRequest request)
        {
            if (!ObjectId.TryParse(request.CreatedBy, out _))
            {
                throw new FormatException("Invalid created by value!");
            }

            var user = await _userRepository.FindFirst(u => u.Id == request.CreatedBy);
            if (user == null)
            {
                throw new KeyNotFoundException("User is not existed!");
            }
            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                Images = request.Images,
                Location = request.Location,
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
                Response = ModelMapper.MapPostToDTO(post, user)
            };
        }

        public async Task<PostListResponse> GetAllPosts()
        {
            var posts = await _postRepository.GetAll();
            List<PostDTO> postsList = new List<PostDTO>();
            foreach (Post p in posts.Where(p => !p.IsDeleted))
            {
                    var postDTO = await FinalMapPostToDTO(p);
                    postsList.Add(postDTO);
            }
            return new PostListResponse
            {
                Success = true,
                Message = "Posts retrieved successfully",
                Response = postsList
            };
        }

        public async Task<PostResponse> GetPostById(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid post id!");
            }
            var post = await _postRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (post == null)
            {
                throw new KeyNotFoundException("Post not found!");
            }



            return new PostResponse
            {
                Success = true,
                Message = "Post retrieved successfully",
                Response = await FinalMapPostToDTO(post)
            };
        }

        private async Task<PostDTO> FinalMapPostToDTO(Post post)
        {
            // get user of the post
            var user = await _userRepository.FindFirst(u => u.Id == post.CreatedBy);
            if (user == null)
            {
                throw new KeyNotFoundException("User is not existed!");
            }

            // count likes
            FilterDefinition<Reaction>? filterLikes = Builders<Reaction>.Filter.Eq(i => i.PostId, post.Id);
            int likes = await _reactionRepository.Count(filterLikes);

            // count comments
            FilterDefinition<Comment>? filterComments = Builders<Comment>.Filter.Eq(i => i.PostId, post.Id);
            int comments = await _commentRepository.Count(filterComments);

            // time
            string time = TimeUtils.GetTimeElapsed((DateTime)post.CreatedAt);

            return ModelMapper.MapPostToDTO(post, user, likes, comments, time);
        }
        public async Task UpdatePost(string id, UpdatePostRequest request)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid post id!");
            }
            var post = await _postRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (post == null) return;

            if (!string.IsNullOrEmpty(request.UpdatedBy) && (!ObjectId.TryParse(request.UpdatedBy, out _) || !request.UpdatedBy.Equals(post.CreatedBy)))
            {
                throw new FormatException("Invalid user try to update post");
            }
            post.Title = request.Title ?? post.Title;
            post.Content = request.Content ?? post.Content;
            post.Images = request.Images ?? post.Images;
            post.UpdatedAt = DateTime.UtcNow;

            await _postRepository.Update(post);
        }

        public async Task DeletePost(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new FormatException("Invalid post id!");
            }
            var post = await _postRepository.FindFirst(t => t.Id == id && !t.IsDeleted);
            if (post == null) return;

            post.IsDeleted = true;
            post.UpdatedAt = DateTime.UtcNow;

            await _postRepository.Update(post);
        }

        public async Task<PostListResponse> GetAllPostsByUserId (string userId)
        {
            if (!ObjectId.TryParse(userId, out _))
            {
                throw new FormatException("Invalid user id!");
            }

            FilterDefinition<Post>? filter = Builders<Post>.Filter.And(
                Builders<Post>.Filter.Eq(p => p.IsDeleted, false), 
                Builders<Post>.Filter.Eq(p => p.CreatedBy, userId));
            var posts = await _postRepository.Search(filter);

            var user = await _userRepository.FindFirst(u => u.Id == userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User is not existed!");
            }
            List<PostDTO> postsList = new List<PostDTO>();
            foreach (Post p in posts)
            {
                // count likes
                FilterDefinition<Reaction>? filterLikes = Builders<Reaction>.Filter.Eq(i => i.PostId, p.Id);
                int likes = await _reactionRepository.Count(filterLikes);

                // count comments
                FilterDefinition<Comment>? filterComments = Builders<Comment>.Filter.Eq(i => i.PostId, p.Id);
                int comments = await _commentRepository.Count(filterComments);

                // time
                string time = TimeUtils.GetTimeElapsed((DateTime)p.CreatedAt);
                postsList.Add(ModelMapper.MapPostToDTO(p, user, likes, comments, time));
            }
            return new PostListResponse
            {
                Success = true,
                Message = "Posts of user retrieved successfully",
                Response = postsList
            };
        }
    }
}
