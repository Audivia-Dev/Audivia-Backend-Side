using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.UserVoucher
{
    public class CreateUserVoucherRequest
    {
        public string? UserId { get; set; }
        public string? VoucherId { get; set; }
        public DateTime? UsedAt { get; set; }
    }
}
