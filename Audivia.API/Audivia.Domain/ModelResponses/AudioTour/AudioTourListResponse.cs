using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.AudioTour
{
    public class AudioTourListResponse : AbstractApiResponse<List<AudioTourDTO>>
    {
        public override List<AudioTourDTO> Response { get; set; }
    }
}
