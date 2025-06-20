namespace Audivia.Domain.ModelRequests.Statistics
{
    public class GetPostStatRequest
    {
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        /// <summary>
        /// Grouping criteria.
        /// Supported values: "day", "month", "year", "user_age_group".
        /// </summary>
        public string? GroupBy { get; set; }
    }
}
