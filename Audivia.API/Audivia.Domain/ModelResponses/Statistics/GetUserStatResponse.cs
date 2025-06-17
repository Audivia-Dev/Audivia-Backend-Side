namespace Audivia.Domain.ModelResponses.Statistics
{
    public class GetUserStatResponse
    {
        public List<UserStatItem> Items { get; set; }
    }

    public class UserStatItem
    {
        public string GroupKey { get; set; }
        public int Count { get; set; }
    }
}
