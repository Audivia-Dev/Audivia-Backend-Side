using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.CheckpointAudio
{
    public class CheckpointAudioResponse : AbstractApiResponse<CheckpointAudioDTO>
    {
        public override CheckpointAudioDTO Response { get; set; }
    }
}
