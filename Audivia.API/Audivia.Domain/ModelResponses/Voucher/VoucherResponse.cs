using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.Voucher
{
    public class VoucherResponse : AbstractApiResponse<VoucherDTO>
    {
        public override VoucherDTO Response { get; set; }
    }
}
