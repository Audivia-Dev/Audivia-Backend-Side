using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.Post
{
    public class PostResponse : AbstractApiResponse<PostDTO>
    {
        public override PostDTO Response { get; set; }
    }
}
