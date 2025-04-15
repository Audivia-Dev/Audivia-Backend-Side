using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.Quiz
{
    public class QuizListResponse : AbstractApiResponse<List<QuizDTO>>
    {
        public override List<QuizDTO> Response { get; set; }
    }
}
