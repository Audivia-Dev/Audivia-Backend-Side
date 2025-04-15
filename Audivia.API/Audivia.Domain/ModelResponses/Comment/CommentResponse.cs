using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.Comment
{
    public class CommentResponse : AbstractApiResponse<CommentDTO>
    {
        public override CommentDTO Response { get; set; }
    }
}
