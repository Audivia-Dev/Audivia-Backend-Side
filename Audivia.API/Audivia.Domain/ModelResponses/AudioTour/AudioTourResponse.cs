using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.AudioTour
{
    public class AudioTourResponse : AbstractApiResponse<AudioTourDTO>
    {
        public override AudioTourDTO Response { get; set; }
    }
}
