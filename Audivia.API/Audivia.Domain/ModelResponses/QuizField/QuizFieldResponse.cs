using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.QuizField
{
    public class QuizFieldResponse : AbstractApiResponse<QuizFieldDTO>
    {
        public override QuizFieldDTO Response { get; set; }
    }


}
