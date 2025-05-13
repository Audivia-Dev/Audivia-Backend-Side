using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.Payment
{
    public class PayOSWebhookData
    {
        public int OrderCode { get; set; }
        public int Amount { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
    }
}
