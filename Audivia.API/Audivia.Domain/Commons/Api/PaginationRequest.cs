namespace Audivia.Domain.Commons.Api
{
    public class PaginationRequest
    {
        public int? Top { get; set; }
        public int? PageIndex { get; set; } = 1;
        public int? PageSize { get; set; } = 5;
    }
}
