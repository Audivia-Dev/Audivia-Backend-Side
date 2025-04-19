using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.UserResponse
{
    public class UpdateUserResponseRequest
    {
        public string? UserId { get; set; }
        public string? QuestionId { get; set; }
        public string? AnswerId { get; set; }
    }
}
