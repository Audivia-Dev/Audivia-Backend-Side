namespace Audivia.Domain.ModelRequests.Comment
{
    public class CreateCommentRequest
    {
        public string? Content { get; set; }
        public string? PostId { get; set; }
        public string? CreatedBy { get; set; }
    }
}
