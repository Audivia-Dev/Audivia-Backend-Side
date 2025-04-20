namespace Audivia.Domain.ModelRequests.UserAudioTour
{
    public class CreateUserAudioTourRequest
    {
        public string? UserId { get; set; }
        public string? TourId { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
