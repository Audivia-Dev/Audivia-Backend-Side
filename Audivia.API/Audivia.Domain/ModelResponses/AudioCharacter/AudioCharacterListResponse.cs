using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.AudioCharacter
{
    public class AudioCharacterListResponse : AbstractApiResponse<List<AudioCharacterDTO>>
    {
        public override List<AudioCharacterDTO> Response { get; set; }
    }
}
