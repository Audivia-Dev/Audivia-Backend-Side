using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.Auth
{
    public class ResetPasswordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
