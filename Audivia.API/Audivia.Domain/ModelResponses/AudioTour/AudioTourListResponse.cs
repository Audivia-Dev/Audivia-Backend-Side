using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.AudioTour
{
    public class AudioTourListResponse : AbstractApiResponse<List<AudioTourDTO>>
    {
        public override List<AudioTourDTO> Response { get; set; }
    }
}
