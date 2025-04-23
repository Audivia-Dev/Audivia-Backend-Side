namespace Audivia.Domain.ModelRequests.TourReview
{
    public class CreateTourReviewRequest
    {
        public string? Title { get; set; }

        public string? ImageUrl { get; set; }

        public string? Content { get; set; }

        public int? Rating { get; set; }

        public required string TourId { get; set; }

        public required string CreatedBy { get; set; }
    }
}
