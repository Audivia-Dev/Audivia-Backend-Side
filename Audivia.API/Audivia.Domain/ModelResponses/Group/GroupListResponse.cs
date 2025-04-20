using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.Group
{
    public class GroupListResponse : AbstractApiResponse<List<GroupDTO>>
    {
        public override List<GroupDTO> Response { get; set; }
    }
}
