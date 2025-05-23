﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.PaymentTransaction
{
    public class CreatePaymentTransactionRequest
    {
        public int OrderCode { get; set; }
        public string UserId { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; } // PENDING, PAID
    }
}
