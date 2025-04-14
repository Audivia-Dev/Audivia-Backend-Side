using Audivia.Domain.DTOs;
using Audivia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Domain.Commons.Mapper
{
    public static class ModelMapper
    {
        public static AudioTourDTO MapAudioTourToDTO(AudioTour tour)
        {
            return new AudioTourDTO
            {
                Id = tour.Id,
                Title = tour.Title,
                Description = tour.Description,
                Price = tour.Price,
                Duration = tour.Duration,
                TypeId = tour.TypeId,
                ThumbnailUrl = tour.ThumbnailUrl,
                IsDeleted = tour.IsDeleted,
                CreatedAt = tour.CreatedAt,
                UpdatedAt = tour.UpdatedAt
            };
        }

        public static QuizFieldDTO MapQuizFieldToDTO(QuizField quizField)
        {
            return new QuizFieldDTO
            {
                Id = quizField.Id,
                Name = quizField.QuizFieldName,
                Description = quizField.Description,
                IsDeleted = quizField.IsDeleted,

            };
        }
    }
}
