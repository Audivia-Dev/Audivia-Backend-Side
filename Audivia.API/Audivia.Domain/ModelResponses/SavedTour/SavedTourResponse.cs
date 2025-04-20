using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.SavedTour
{
    public class SavedTourResponse : AbstractApiResponse<SavedTourDTO>
    {
        public override SavedTourDTO Response { get; set; }
    }
}
