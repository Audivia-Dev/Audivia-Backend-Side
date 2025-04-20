using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.TourReview
{
    public class TourReviewResponse : AbstractApiResponse<TourReviewDTO>
    {
        public override TourReviewDTO Response { get; set; }
    }
}
