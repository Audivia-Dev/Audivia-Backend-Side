using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.UserCurrentLocation
{
    public class UserCurrentLocationListResponse : AbstractApiResponse<List<UserCurrentLocationDTO>>
    {
        public override List<UserCurrentLocationDTO> Response { get; set; }
    }
}
