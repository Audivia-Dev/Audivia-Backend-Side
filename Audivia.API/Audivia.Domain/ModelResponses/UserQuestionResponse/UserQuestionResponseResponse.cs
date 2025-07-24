using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.UserQuestionResponse
{
    public class UserQuestionResponseResponse : AbstractApiResponse<UserQuestionResponseDTO>
    {
        public override UserQuestionResponseDTO Response { get; set; }
    }
}
