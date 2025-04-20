using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.SavedTour
{
    public class SavedTourListResponse : AbstractApiResponse<List<SavedTourDTO>>
    {
        public override List<SavedTourDTO> Response { get; set; }
    }
}
