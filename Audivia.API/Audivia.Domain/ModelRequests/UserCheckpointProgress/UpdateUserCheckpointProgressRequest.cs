namespace Audivia.Domain.ModelRequests.UserCheckpointProgress
{
    public class UpdateUserCheckpointProgressRequest
    {
        public int? ProgressSeconds { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? LastListenedTime { get; set; }
    }
}
