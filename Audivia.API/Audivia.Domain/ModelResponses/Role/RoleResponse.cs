using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.Role
{
    public class RoleResponse : AbstractApiResponse<RoleDTO>
    {
        public override RoleDTO Response { get; set; }
    }

}
