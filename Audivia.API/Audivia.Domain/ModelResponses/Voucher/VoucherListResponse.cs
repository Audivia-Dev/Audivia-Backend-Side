using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.Voucher
{
    public class VoucherListResponse : AbstractApiResponse<List<VoucherDTO>>
    {
        public override List<VoucherDTO> Response { get; set; }
    }
}
