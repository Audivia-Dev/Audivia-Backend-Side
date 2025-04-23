using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.Payment
{
    public class PaymentResponse
    {
        public string Id { get; set; }
        public int OrderCode { get; set; }
        public string Status { get; set; }
        public int  Amount { get; set; }
    }
}
