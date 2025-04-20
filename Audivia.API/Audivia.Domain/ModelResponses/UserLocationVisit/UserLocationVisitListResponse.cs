using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.UserLocationVisit
{
    public class UserLocationVisitListResponse : AbstractApiResponse<List<UserLocationVisitDTO>>
    {
        public override List<UserLocationVisitDTO> Response { get; set; }
    }
}
