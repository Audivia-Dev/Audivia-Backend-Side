namespace Audivia.Domain.ModelResponses.Statistics
{
    public class GetRevenueStatResponse
    {
        public List<RevenueStatItem> Items { get; set; }
    }

    public class RevenueStatItem
    {
        public string GroupKey {  get; set; }
        public double Revenue { get; set; }
    }

}
