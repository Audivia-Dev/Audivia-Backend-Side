using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.Voucher
{
    public class UpdateVoucherRequest
    {
        public string? Code { get; set; }
        public float? Discount { get; set; }
        public string? Title { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
