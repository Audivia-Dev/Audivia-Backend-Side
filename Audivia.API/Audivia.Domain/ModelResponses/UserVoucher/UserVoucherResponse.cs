using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.UserVoucher
{
    public class UserVoucherResponse : AbstractApiResponse<UserVoucherDTO>
    {
        public override UserVoucherDTO Response { get; set; }
    }
}
