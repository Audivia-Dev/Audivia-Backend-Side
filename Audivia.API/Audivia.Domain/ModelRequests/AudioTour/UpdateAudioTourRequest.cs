

namespace Audivia.Domain.ModelRequests.AudioTour
{
    public class UpdateAudioTourRequest
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? Duration { get; set; }

        public string? TypeId { get; set; }

        public string? ThumbnailUrl { get; set; }
    }
}
