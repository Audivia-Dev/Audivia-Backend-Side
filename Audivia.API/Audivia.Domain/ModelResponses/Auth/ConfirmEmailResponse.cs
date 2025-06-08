namespace Audivia.Domain.ModelResponses.Auth
{
    public class ConfirmEmailResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
    }
}
