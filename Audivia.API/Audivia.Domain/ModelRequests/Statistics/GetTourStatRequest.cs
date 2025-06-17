namespace Audivia.Domain.ModelRequests.Statistics
{
    public class GetTourStatRequest
    {
        /// <summary>
        /// The criteria to group tours by.
        /// Supported values: "tour_type", "rating_group".
        /// </summary>
        public string? GroupBy { get; set; }
    }
}
