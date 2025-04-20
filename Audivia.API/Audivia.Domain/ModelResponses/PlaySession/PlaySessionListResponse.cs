using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.PlaySession
{
    public class PlaySessionListResponse : AbstractApiResponse<List<PlaySessionDTO>>
    {
        public override List<PlaySessionDTO> Response { get; set; }
    }
}
