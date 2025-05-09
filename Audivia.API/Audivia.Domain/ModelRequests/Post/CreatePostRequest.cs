namespace Audivia.Domain.ModelRequests.Post
{
    public class CreatePostRequest
    {
        public string? Title { get; set; }

        public string[]? Images { get; set; }

        public string? Location { get; set; }

        public string? Content { get; set; }

        public string? CreatedBy { get; set; }
    }
}
