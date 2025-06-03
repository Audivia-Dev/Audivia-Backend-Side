using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.UserCheckpointProgress
{
    public class UserCheckpointProgressResponse : AbstractApiResponse<UserCheckpointProgressDTO>
    {
        public override UserCheckpointProgressDTO Response { get; set; }
    }
}
