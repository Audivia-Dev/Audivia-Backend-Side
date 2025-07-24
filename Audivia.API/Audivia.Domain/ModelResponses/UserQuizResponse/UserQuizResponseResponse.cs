using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.UserQuizResponse
{
    public class UserQuizResponseResponse : AbstractApiResponse<UserQuizResponseDTO>
    {
        public override UserQuizResponseDTO Response { get; set; }
    }
}
