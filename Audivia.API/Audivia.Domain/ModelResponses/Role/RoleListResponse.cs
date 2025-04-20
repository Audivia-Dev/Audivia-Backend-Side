using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.Role
{
    public class RoleListResponse : AbstractApiResponse<List<RoleDTO>>
    {
        public override List<RoleDTO> Response { get; set; }
    }
}
