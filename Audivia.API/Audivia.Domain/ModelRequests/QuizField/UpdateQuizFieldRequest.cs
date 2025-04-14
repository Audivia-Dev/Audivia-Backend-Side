using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.QuizField
{
    public class UpdateQuizFieldRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
