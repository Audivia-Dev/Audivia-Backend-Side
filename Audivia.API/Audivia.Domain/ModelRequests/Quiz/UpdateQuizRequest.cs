using Audivia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.ModelRequests.Quiz
{
    public class UpdateQuizRequest
    {
        public string? QuizFieldId { get; set; }

        public string? TourId { get; set; }
        public string? Image { get; set; }
        public string? Title { get; set; }
    }
}
