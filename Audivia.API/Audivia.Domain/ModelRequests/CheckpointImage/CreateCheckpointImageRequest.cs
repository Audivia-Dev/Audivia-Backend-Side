using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.CheckpointImage
{
    public class CreateCheckpointImageRequest
    {
        public string? TourCheckpointId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
    }
}
