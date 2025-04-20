using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.Group
{
    public class GroupResponse : AbstractApiResponse<GroupDTO>
    {
        public override GroupDTO Response { get; set; }
    }
}
