using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.PlayResult
{
    public class PlayResultResponse : AbstractApiResponse<PlayResultDTO>
    {
        public override PlayResultDTO Response { get; set; }
    }
}
