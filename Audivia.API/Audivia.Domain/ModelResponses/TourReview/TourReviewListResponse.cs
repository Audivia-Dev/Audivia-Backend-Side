using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.TourReview
{
    public class TourReviewListResponse : AbstractApiResponse<List<TourReviewDTO>>
    {
        public override List<TourReviewDTO> Response { get; set; }
    }
}
