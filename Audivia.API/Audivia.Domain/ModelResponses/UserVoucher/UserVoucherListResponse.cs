using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.UserVoucher
{
    public class UserVoucherListResponse : AbstractApiResponse<List<UserVoucherDTO>>
    {
        public override List<UserVoucherDTO> Response { get; set; }
    }
}
