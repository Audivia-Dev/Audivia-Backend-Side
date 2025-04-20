using Audivia.Domain.ModelRequests.Group;
using Audivia.Domain.ModelResponses.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Interface
{
    public interface IGroupService
    {
        Task<GroupResponse> CreateGroupAsync(CreateGroupRequest req);
        Task<GroupResponse> UpdateGroupAsync(string id, UpdateGroupRequest req);
        Task<GroupResponse> DeleteGroupAsync(string id);
        Task<GroupListResponse> GetAllGroupsAsync();
        Task<GroupResponse> GetGroupByIdAsync(string id);
    }
}
