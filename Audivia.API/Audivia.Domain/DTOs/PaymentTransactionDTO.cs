using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.DTOs
{
    public class PaymentTransactionDTO
    {
        public string Id { get; set; }
        public int OrderCode { get; set; }
        public string UserId { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; } // PENDING, PAID
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaymentTime { get; set; }
    }
}
