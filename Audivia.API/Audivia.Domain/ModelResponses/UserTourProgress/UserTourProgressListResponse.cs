using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.UserTourProgress
{
    public class UserTourProgressListResponse : AbstractApiResponse<List<UserTourProgressDTO>>
    {
        public override List<UserTourProgressDTO> Response { get; set; }
    }
}
