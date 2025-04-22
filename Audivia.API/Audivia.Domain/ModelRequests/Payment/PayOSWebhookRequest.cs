using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.Payment
{
    public class PayOSWebhookRequest
    {
        public string OrderCode { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public long PaymentTime { get; set; }
        public string Signature { get; set; }
    }
}
