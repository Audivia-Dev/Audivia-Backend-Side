using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.AudioTour
{
    public class AudioTourListResponse : AbstractApiResponse<List<TourDTO>>
    {
        public override List<TourDTO> Response { get; set; }
    }
}
