namespace Audivia.Domain.ModelRequests.Statistics
{
    public class GetRevenueStatRequest
    {

        // time interval
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        // group by (day/month/year/tour/user_age_group)
        public string? GroupBy { get; set; }
        public int? Top { get; set; } // top sort by group by

    }
}
