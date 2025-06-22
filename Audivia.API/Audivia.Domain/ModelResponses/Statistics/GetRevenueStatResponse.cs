namespace Audivia.Domain.ModelResponses.Statistics
{
    public class GetRevenueStatResponse
    {
        public int TotalBookings { get; set; }
        public double TotalRevenue { get; set; }
        public List<RevenueStatItem> Items { get; set; }
    }

    public class RevenueStatItem
    {
        public string GroupKey {  get; set; }
        public double Revenue { get; set; }
    }

}
