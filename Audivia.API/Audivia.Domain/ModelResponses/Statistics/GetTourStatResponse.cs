namespace Audivia.Domain.ModelResponses.Statistics
{
    public class GetTourStatResponse
    {
        public int TotalTours { get; set; }
        public List<TourStatItem> Items { get; set; }
    }

    public class TourStatItem
    {
        public string GroupKey {  get; set; }
        public int Value { get; set; }
    }
}
