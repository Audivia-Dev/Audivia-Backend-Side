namespace Audivia.Domain.ModelRequests.Post
{
    public class UpdatePostRequest
    {
        public string? Title { get; set; }

        public string[]? Images { get; set; }

        public string? Content { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
