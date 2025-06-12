using Audivia.Domain.Commons.Api;

namespace Audivia.Domain.ModelRequests.Tour
{
    public class GetToursRequest : PaginationRequest
    {
        public string? TourTypeId { get; set; }
        public string? TourTypeName { get; set; }
        public string? Title { get; set; }
        public string? Sort { get; set; }
    }
}
