using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.Route
{
    public class UpdateRouteRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
