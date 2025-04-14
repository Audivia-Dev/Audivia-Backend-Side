using Audivia.Commons.Api;
using Audivia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelResponses.QuizField
{
    public class QuizFieldListResponse : AbstractApiResponse<List<QuizFieldDTO>>
    {
        public override List<QuizFieldDTO> Response { get; set; }
    }
}
