using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.GroupMember
{
    public class UpdateGroupMemberRequest
    {
        public string? UserId { get; set; }
        public string? GroupId { get; set; }
    }
}
