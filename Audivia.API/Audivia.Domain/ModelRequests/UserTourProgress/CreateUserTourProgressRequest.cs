using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Audivia.Domain.ModelRequests.UserTourProgress
{
    public class CreateUserTourProgressRequest
    {
        public string? TourId { get; set; }
        public string? UserId { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string? CurrentCheckpointId { get; set; }
        public int? Score { get; set; } = 0;
        public bool GroupMode { get; set; } = false;
        public string? GroupId { get; set; }       
    }
}
