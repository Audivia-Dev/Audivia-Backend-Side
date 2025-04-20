using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.Comment
{
    public class CommentListResponse : AbstractApiResponse<List<CommentDTO>>
    {
        public override List<CommentDTO> Response { get; set; }
    }
}
