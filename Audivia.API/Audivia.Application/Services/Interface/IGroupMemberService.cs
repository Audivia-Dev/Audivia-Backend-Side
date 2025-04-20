using Audivia.Domain.ModelRequests.GroupMember;
using Audivia.Domain.ModelResponses.GroupMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IGroupMemberService
    {
        Task<GroupMemberResponse> CreateGroupMemberAsync(CreateGroupMemberRequest req);
        Task<GroupMemberResponse> UpdateGroupMemberAsync(string id, UpdateGroupMemberRequest req);
        Task<GroupMemberResponse> DeleteGroupMemberAsync(string id);
        Task<GroupMemberListResponse> GetAllGroupMembersAsync();
        Task<GroupMemberResponse> GetGroupMemberByIdAsync(string id);
    }
}
