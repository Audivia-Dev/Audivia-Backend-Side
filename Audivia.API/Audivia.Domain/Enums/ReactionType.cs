using System.Runtime.Serialization;

namespace Audivia.Domain.Enums
{
    public enum ReactionType
    {
        [EnumMember(Value ="like")]
        Like,

        [EnumMember(Value = "dislike")]
        Dislike,
    }
}
