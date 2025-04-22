using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.PaymentTransaction
{
    public class PaymentTransactionResponse : AbstractApiResponse<PaymentTransactionDTO>
    {
        public override PaymentTransactionDTO Response { get; set; }
    }
}
