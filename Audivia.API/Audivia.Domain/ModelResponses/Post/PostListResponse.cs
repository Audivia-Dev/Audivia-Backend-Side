using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.Post
{
    public class PostListResponse : AbstractApiResponse<List<PostDTO>>
    {
        public override List<PostDTO> Response { get; set; }
    }
}
