using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.CheckpointImage
{
    public class CheckpointImageListResponse : AbstractApiResponse<List<CheckpointImageDTO>>
    {
        public override List<CheckpointImageDTO> Response { get; set; }
    }
}
