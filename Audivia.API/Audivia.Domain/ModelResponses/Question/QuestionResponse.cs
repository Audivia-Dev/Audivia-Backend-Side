using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.Question
{
    public class QuestionResponse : AbstractApiResponse<QuestionDTO>
    {
        public override QuestionDTO Response { get; set; }
    }
}
