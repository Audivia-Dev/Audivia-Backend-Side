using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.CheckpointImage
{
    public class UpdateCheckpointImageRequest
    {
        public string? TourCheckpointId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
    }
}
