namespace Audivia.Domain.ModelRequests.AudioCharacter
{
    public class UpdateAudioCharacterRequest
    {
        public string? Name { get; set; }
        public string? VoiceType { get; set; }
        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
