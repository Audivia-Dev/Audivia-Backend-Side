namespace Audivia.Domain.ModelRequests.AudioCharacter
{
    public class CreateAudioCharacterRequest
    {
        public string Name { get; set; }
        public string VoiceType { get; set; }
        public string Description { get; set; }
        public string AvatarUrl { get; set; }
        public string AudioUrl { get; set; }
    }
}
