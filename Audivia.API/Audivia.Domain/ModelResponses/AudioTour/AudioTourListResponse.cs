using Audivia.Commons.Api;
using Audivia.Domain.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.AudioTour
{
    public class AudioTourListResponse : AbstractApiResponse<PaginationResponse<TourDTO>>
    {
        public override PaginationResponse<TourDTO> Response { get; set; }
    }
}
