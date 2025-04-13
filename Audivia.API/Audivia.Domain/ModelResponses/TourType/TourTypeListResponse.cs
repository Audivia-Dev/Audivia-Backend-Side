using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.TourType
{
    public class TourTypeListResponse : AbstractApiResponse<List<TourTypeDTO>>
    {
        public override List<TourTypeDTO> Response { get; set; }
    }
}
