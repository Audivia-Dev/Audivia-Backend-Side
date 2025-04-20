using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.GroupMember
{
    public class GroupMemberListResponse : AbstractApiResponse<List<GroupMemberDTO>>
    {
        public override List<GroupMemberDTO> Response { get; set; }
    }
}
