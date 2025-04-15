using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.Role;
using Audivia.Domain.ModelResponses.Role;

namespace Audivia.Application.Services.Interface
{
    public interface IRoleService
    {
        Task<RoleResponse> CreateRole(CreateRoleRequest request);

        Task<List<RoleDTO>> GetAllRoles();

        Task<RoleResponse> GetRoleById(string id);

        Task UpdateRole(string id, UpdateRoleRequest request);

        Task DeleteRole(string id);
    }
}
