using Audivia.Domain.DTOs;
using Audivia.Domain.Models;
using System.Data;

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
        public static RoleDTO MapRoleToDTO(Role role)
        {
            return new RoleDTO
            {
                Id = role.Id,
                RoleName = role.RoleName,
                IsDeleted = role.IsDeleted,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt
            };
        }

        public static PostDTO MapPostToDTO(Post post)
        {
            return new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ImageUrl = post.ImageUrl,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt,
                CreatedBy = post.CreatedBy,
                IsDeleted = post.IsDeleted
            };
        }

        public static TourReviewDTO MapTourReviewToDTO(TourReview review)
        {
            return new TourReviewDTO
            {
                Id = review.Id,
                Title = review.Title,
                Content = review.Content,
                Rating = review.Rating,
                ImageUrl = review.ImageUrl,
                TourId = review.TourId,
                CreatedAt = review.CreatedAt,
                UpdatedAt = review.UpdatedAt,
                CreatedBy = review.CreatedBy,
                IsDeleted = review.IsDeleted
            };
        }

        public static CommentDTO MapCommentToDTO(Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt,
                CreatedBy = comment.CreatedBy,
                IsDeleted = comment.IsDeleted,
                PostId = comment.PostId,
            };
        }

        public static ReactionDTO MapReactionToDTO(Reaction reaction)
        {
            return new ReactionDTO
            {
                Id = reaction.Id,
                Type = reaction.Type.ToString(),
                PostId = reaction.PostId,
                CreatedAt = reaction.CreatedAt,
                CreatedBy = reaction.CreatedBy,
            };
        }

        public static QuizDTO MapQuizToDTO(Quiz q)
        {
            return new QuizDTO
            {
                Id = q.Id,
                Title = q.Title,
                QuizFieldId = q.QuizFieldId,
                TourCheckpointId = q.TourCheckpointId,
                CreatedAt = q.CreatedAt,    
                UpdatedAt = q.UpdatedAt,
                IsDeleted = q.IsDeleted,
                Image = q.Image,
            };
        }
        public static QuestionDTO MapQuestionToDTO(Question question)
        {
            return new QuestionDTO
            {
                Id = question.Id,
                Text = question.Text,
                QuizId = question.QuizId,
                Points = question.Points,
                Type = question.Type,
                CreatedAt = question.CreatedAt,
                UpdatedAt = question.UpdatedAt,
                IsDeleted = question.IsDeleted,
                Answers = question.Answers?.Select(MapAnswerToDTO).ToList()  // Correct mapping
            };
        }
        public static AnswerDTO MapAnswerToDTO(Answer a)
        {
            return new AnswerDTO
            {
                Id = a.Id,
                QuestionId = a.QuestionId, 
                Text = a.Text,
                IsCorrect = a.IsCorrect,
                IsDeleted = a.IsDeleted,
            };
        }

        public static UserResponseDTO MapUserResponseToDTO(UserResponse userResponse)
        {
            return new UserResponseDTO
            {
                Id = userResponse.Id,
                AnswerId = userResponse.AnswerId,
                QuestionId = userResponse.QuestionId,
                RespondedAt = userResponse.RespondedAt,
                UserId = userResponse.UserId,
            };
        }

        public static RouteDTO MapRouteToDTO(Route r)
        {
            return new RouteDTO
            {
                Id=r.Id,
                Description=r.Description,
                Name = r.Name,
                IsDeleted = r.IsDeleted,
            };
        }
        public static UserLocationVisitDTO MapUserLocationVisitToDTO(UserLocationVisit userLocationVisit)
        {
            return new UserLocationVisitDTO
            {
                Id = userLocationVisit.Id,
                UserId=userLocationVisit.UserId,
                IsDeleted=userLocationVisit.IsDeleted,
                RouteId = userLocationVisit.RouteId,
                TourcheckpointId = userLocationVisit.TourcheckpointId,
                VisitedAt = userLocationVisit.VisitedAt,
                
            };
        }
        public static UserCurrentLocationDTO MapUserCurrentLocationToDTO(UserCurrentLocation userCurrentLocation)
        {
            return new UserCurrentLocationDTO
            {
                Id = userCurrentLocation.Id,
                Latitude = userCurrentLocation.Latitude,
                Longitude = userCurrentLocation.Longitude,
                UpdatedAt = userCurrentLocation.UpdatedAt,
                UserId = userCurrentLocation.UserId,
                Accuracy = userCurrentLocation.Accuracy,
            };
        }
        public static UserCurrentLocationDTO MapUserResponseToDTO(UserCurrentLocation userCurrentLocation)
        {
            return new UserCurrentLocationDTO
            {
                Id = userCurrentLocation.Id,
                Accuracy = userCurrentLocation.Accuracy,
                Latitude = userCurrentLocation.Latitude,
                Longitude = userCurrentLocation.Longitude,
                UpdatedAt = userCurrentLocation.UpdatedAt,
                UserId = userCurrentLocation.UserId,
                
            };
        }
    }
}
