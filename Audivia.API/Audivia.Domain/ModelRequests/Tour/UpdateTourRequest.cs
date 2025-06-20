

using Audivia.Domain.Models;

namespace Audivia.Domain.ModelRequests.AudioTour
{
    public class UpdateTourRequest
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Location { get; set; }

        public double? StartLatitude { get; set; }

        public double? StartLongitude { get; set; }

        public decimal? Price { get; set; }

        public int? Duration { get; set; }

        public string? TypeId { get; set; }

        public string? ThumbnailUrl { get; set; }

        public bool? UseCustomMap { get; set; }

        public List<CustomMap>? CustomMapImages { get; set; }
    }
}
