using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.Quiz
{
    public class QuizReponse :  AbstractApiResponse<QuizDTO>
    {
        public override QuizDTO Response { get; set; }
    }
}
