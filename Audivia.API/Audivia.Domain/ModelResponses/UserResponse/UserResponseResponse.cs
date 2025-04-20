using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.UserResponse
{
    public class UserResponseResponse : AbstractApiResponse<UserResponseDTO>
    {
        public override UserResponseDTO Response { get; set; }
    }
}
