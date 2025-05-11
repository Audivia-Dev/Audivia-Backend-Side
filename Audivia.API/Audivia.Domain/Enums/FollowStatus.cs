using System.Runtime.Serialization;

namespace Audivia.Domain.Enums
{
    public enum FollowStatus
    {
        [EnumMember(Value = "not_following")]
        NotFollowing,

        [EnumMember(Value = "following")]
        Following,

        [EnumMember(Value = "friends")]
        Friends
    }
}
