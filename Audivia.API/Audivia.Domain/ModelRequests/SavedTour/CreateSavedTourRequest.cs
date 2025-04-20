namespace Audivia.Domain.ModelRequests.SavedTour
{
    public class CreateSavedTourRequest
    {
        public string? UserId { get; set; }
        public string? TourId { get; set; }
        public DateTime? PlannedTime { get; set; }
    }
}
