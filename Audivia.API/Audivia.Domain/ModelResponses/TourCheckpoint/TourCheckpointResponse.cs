using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.TourCheckpoint
{
    public class TourCheckpointResponse : AbstractApiResponse<TourCheckpointDTO>
    {
        public override TourCheckpointDTO Response { get; set; }
    }
    
    
}
