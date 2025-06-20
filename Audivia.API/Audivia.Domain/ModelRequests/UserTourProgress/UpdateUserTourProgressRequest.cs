namespace Audivia.Domain.ModelRequests.UserTourProgress
{
    public class UpdateUserTourProgressRequest
    {
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public bool? IsCompleted { get; set; }
        public string? CurrentCheckpointId { get; set; }
        public int? Score { get; set; }
        public bool? GroupMode { get; set; }
        public string? GroupId { get; set; }
    }
}
