using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.UserQuizResponse
{
    public class UserQuizResponseListResponse : AbstractApiResponse<List<UserQuizResponseDTO>>
    {
        public override List<UserQuizResponseDTO> Response { get; set; }
    }
}
