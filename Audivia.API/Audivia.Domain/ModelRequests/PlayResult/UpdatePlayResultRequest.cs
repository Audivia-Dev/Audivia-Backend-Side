using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.PlayResult
{
    public class UpdatePlayResultRequest
    {
        public string? SessionId { get; set; }
        public double? Score { get; set; }
    }
}
