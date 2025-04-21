using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.AudioCharacter
{
    public class AudioCharacterResponse : AbstractApiResponse<AudioCharacterDTO>
    {
        public override AudioCharacterDTO Response { get; set; }
    }
}
