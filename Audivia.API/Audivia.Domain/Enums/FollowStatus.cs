using System.Runtime.Serialization;

namespace Audivia.Domain.Enums
{
    public enum FollowStatus
    {
        [EnumMember(Value = "not_following")] // Both not follow
        NotFollowing,

        [EnumMember(Value = "following")] // Current User follow Target User
        Following,

        [EnumMember(Value = "friends")] // Both follow => friends
        Friends,

        [EnumMember(Value = "not_followed_back")] // Current User not follow Target User ---BUT--- Target User follow Current User
        NotFollowedBack,
    }
}
