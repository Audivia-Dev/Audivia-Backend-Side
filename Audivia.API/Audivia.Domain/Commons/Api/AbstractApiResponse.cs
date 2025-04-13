namespace Audivia.Commons.Api
{
    public abstract class AbstractApiResponse<U> where U : class
    {
        public string? Message { get; set; }
        public bool Success { get; set; } = false;
        public abstract U Response { get; set; }
    }
}
