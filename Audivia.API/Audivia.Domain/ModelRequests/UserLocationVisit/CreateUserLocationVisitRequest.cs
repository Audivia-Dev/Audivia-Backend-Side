﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.UserLocationVisit
{
    public class CreateUserLocationVisitRequest
    {
        public string? UserId { get; set; }
        public string? TourcheckpointId { get; set; }
    }
}
