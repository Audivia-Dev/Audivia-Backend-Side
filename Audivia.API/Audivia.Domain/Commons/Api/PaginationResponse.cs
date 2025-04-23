namespace Audivia.Domain.Commons.Api
{
    public class PaginationResponse<T>(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
    {
        public int PageIndex { get; set; } = pageIndex;
        public int PageSize { get; set; } = pageSize;
        public int Count { get; set; } = count;
        public int TotalPages { get; set; } = (int)Math.Ceiling((decimal)count / pageSize);
        public IReadOnlyList<T> Data { get; set; } = data;
    }
}
