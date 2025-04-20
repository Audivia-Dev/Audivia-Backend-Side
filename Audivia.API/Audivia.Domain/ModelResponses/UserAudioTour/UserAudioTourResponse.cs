using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.UserAudioTour
{
    public class UserAudioTourResponse : AbstractApiResponse<UserAudioTourDTO>
    {
        public override UserAudioTourDTO Response { get; set; }
    }
}
