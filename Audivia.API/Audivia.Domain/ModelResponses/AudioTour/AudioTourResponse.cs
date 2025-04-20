using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.AudioTour
{
    public class AudioTourResponse : AbstractApiResponse<TourDTO>
    {
        public override TourDTO Response { get; set; }
    }
}
