using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Enums
{
    public enum QuestionType
    {
        [EnumMember(Value = "MultipleChoice")] MultipleChoice,
        [EnumMember(Value = "TrueFalse")] TrueFalse,

    }
}
