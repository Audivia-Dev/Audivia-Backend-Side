using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.GroupMember
{
    public class CreateGroupMemberRequest
    {
        public string? UserId { get; set; }
        public string? GroupId { get; set; }
    }
}
