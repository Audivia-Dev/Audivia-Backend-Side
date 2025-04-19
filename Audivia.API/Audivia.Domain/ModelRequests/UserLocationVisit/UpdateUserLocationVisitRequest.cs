using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.UserLocationVisit
{
    public class UpdateUserLocationVisitRequest
    {
        public string? UserId { get; set; }
        public string? TourcheckpointId { get; set; }
        public string? RouteId { get; set; }
    }
}
