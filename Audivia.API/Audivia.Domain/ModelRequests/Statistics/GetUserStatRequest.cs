namespace Audivia.Domain.ModelRequests.Statistics
{
    public class GetUserStatRequest
    {
        /// <summary>
        /// Type of statistic. Supported values: "new_users" (default), "total_by_age".
        /// "new_users" counts users created in the given time interval.
        /// "total_by_age" counts all users and groups them by age.
        /// </summary>
        public string StatType { get; set; } = "new_users";
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        /// <summary>
        /// Grouping criteria.
        /// For "new_users": "day", "month", "year", "user_age_group".
        /// For "total_by_age": only "user_age_group" is applicable.
        /// </summary>
        public string? GroupBy { get; set; }
    }
}
