using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.Answer
{
    public class AnswerResponse : AbstractApiResponse<AnswerDTO>
    {
        public override AnswerDTO Response { get; set; }
    }
}
