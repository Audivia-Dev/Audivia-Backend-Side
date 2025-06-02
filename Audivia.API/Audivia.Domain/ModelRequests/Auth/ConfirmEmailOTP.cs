using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.Auth
{
    public class ConfirmEmailOTP
    {
        public string Email { get; set; }
        public int Otp { get; set; }
    }
}
