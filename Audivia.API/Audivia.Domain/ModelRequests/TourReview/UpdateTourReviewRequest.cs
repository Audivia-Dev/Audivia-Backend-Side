namespace Audivia.Domain.ModelRequests.TourReview
{
    public class UpdateTourReviewRequest
    {
        public string? Title { get; set; }

        public string? ImageUrl { get; set; }

        public string? Content { get; set; }

        public int? Rating { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
