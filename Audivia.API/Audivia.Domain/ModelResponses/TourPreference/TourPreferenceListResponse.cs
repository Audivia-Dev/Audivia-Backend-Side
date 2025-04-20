using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.TourPreference
{
    public class TourPreferenceListResponse: AbstractApiResponse<List<TourPreferenceDTO>>
    {
        public override List<TourPreferenceDTO> Response { get; set; }
    }
}
