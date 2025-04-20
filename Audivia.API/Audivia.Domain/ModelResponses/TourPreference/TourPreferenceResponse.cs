using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.TourPreference
{
    public class TourPreferenceResponse : AbstractApiResponse<TourPreferenceDTO>
    {
        public override TourPreferenceDTO Response { get; set; }
    }
}
