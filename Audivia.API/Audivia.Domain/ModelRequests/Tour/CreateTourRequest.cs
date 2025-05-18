    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Audivia.Domain.ModelRequests.AudioTour
    {
        public class CreateTourRequest
        {
            public string? Title { get; set; }

            public string? Description { get; set; }

            public string? Location { get; set; }

            public double? StartLatitude { get; set; }
            
            public double? StartLongitude {  get; set; }

            public decimal? Price { get; set; }

            public int? Duration { get; set; }

            public string? TypeId { get; set; }

            public string? ThumbnailUrl { get; set; }
        }
    }
