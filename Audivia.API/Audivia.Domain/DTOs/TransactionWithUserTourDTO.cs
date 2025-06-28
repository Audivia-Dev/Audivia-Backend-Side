using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.DTOs
{
    public class TransactionWithUserTourDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TourId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // User info
        public string Email { get; set; }
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }

    }

}
