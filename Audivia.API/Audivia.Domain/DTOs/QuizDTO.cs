using Audivia.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.DTOs
{
    public class QuizDTO
    {
        public string Id { get; set; } = null!;

        public string? QuizFieldId { get; set; }
        public QuizField? QuizField { get; set; }

        public string? TourCheckpointId { get; set; }
        public TourCheckpoint? TourCheckpoint { get; set; }

        public string? Title { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
