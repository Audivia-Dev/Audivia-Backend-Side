using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.TourType
{
    public class TourTypeResponse : AbstractApiResponse<TourTypeDTO>
    {
        public override TourTypeDTO Response { get; set; }
    }
}
