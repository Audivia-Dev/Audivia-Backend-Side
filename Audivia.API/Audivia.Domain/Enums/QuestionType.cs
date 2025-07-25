using System.Runtime.Serialization;

namespace Audivia.Domain.Enums
{
    public enum QuestionType
    {
        [EnumMember(Value = "MultipleChoice")] MultipleChoice,
        [EnumMember(Value = "TrueFalse")] TrueFalse,

    }
}
