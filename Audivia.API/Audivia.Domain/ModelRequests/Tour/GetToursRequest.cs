using Audivia.Domain.Commons.Api;

namespace Audivia.Domain.ModelRequests.Tour
{
    public class GetToursRequest : PaginationRequest
    {
        public string? Sort { get; set; }
    }
}
