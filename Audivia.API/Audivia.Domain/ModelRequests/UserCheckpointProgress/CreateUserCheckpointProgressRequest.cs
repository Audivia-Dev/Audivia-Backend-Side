namespace Audivia.Domain.ModelRequests.UserCheckpointProgress
{
    public class CreateUserCheckpointProgressRequest
    {
        public string UserTourProgressId { get; set; }
        public string CheckpointAudioId { get; set; }
        public string TourCheckpointId { get; set; }
        public int ProgressSeconds { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? LastListenedTime { get; set; }
    }
}
