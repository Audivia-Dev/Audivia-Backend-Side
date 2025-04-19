using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.Route
{
    public class RouteListResponse : AbstractApiResponse<List<RouteDTO>>
    {
        public override List<RouteDTO> Response { get; set; }
    }
}
