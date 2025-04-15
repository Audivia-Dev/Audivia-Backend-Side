namespace Audivia.Domain.ModelRequests.TourReview
{
    public class CreateTourReviewRequest
    {
        public string? Title { get; set; }

        public string? ImageUrl { get; set; }

        public string? Content { get; set; }

        public int? Rating { get; set; }

        public string? TourId { get; set; }

        public string? CreatedBy { get; set; }
    }
}
