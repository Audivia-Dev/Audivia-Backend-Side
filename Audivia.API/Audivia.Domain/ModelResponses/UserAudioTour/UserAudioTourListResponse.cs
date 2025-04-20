using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
namespace Audivia.Domain.ModelResponses.UserAudioTour
{
    public class UserAudioTourListResponse : AbstractApiResponse<List<UserAudioTourDTO>>
    {
        public override List<UserAudioTourDTO> Response { get; set; }
    }
}
