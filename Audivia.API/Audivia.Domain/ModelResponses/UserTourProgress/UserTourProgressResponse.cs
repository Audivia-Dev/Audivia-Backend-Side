using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.UserTourProgress
{
    public class UserTourProgressResponse : AbstractApiResponse<UserTourProgressDTO>
    {
        public override UserTourProgressDTO Response { get; set; }
    }
}
