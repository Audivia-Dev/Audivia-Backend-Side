namespace Audivia.Domain.ModelResponses.Statistics
{
    public class GetPostStatResponse
    {
        public List<PostStatItem> Items { get; set; }
    }

    public class PostStatItem
    {
        public string GroupKey { get; set; }
        public int Value { get; set; }
    }
}
