namespace Audivia.Domain.ModelRequests.TourPreference
{
    public class CreateTourPreferenceRequest
    {
        public string? UserId { get; set; }
        public string? TourId { get; set; }
        public int? PredictedScore { get; set; }
    }
}
