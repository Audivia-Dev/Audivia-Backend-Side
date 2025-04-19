using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.UserCurrentLocation
{
    public class UserCurrentLocationResponse : AbstractApiResponse<UserCurrentLocationDTO>
    {
        public override UserCurrentLocationDTO Response { get; set; }
    }
}
