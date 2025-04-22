using Audivia.Domain.DTOs;
using Audivia.Domain.Models;
using MongoDB.Bson.Serialization.Serializers;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Audivia.Domain.Commons.Mapper
{
    public static class ModelMapper
    {
        public static UserDTO MapUserToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.Username,
                FullName = user.FullName,
                Phone = user.Phone,
                AvatarUrl = user.AvatarUrl,
                Bio = user.Bio,
                BalanceWallet = user.BalanceWallet,
                AudioCharacterId = user.AudioCharacterId,
                AutoPlayDistance = user.AutoPlayDistance,
                TravelDistance = user.TravelDistance,
                RoleId = user.RoleId,
                ConfirmedEmail = user.ConfirmedEmail,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                IsDeleted = user.IsDeleted,
            };
        }
        public static TourDTO MapAudioTourToDTO(Tour tour)
        {
            return new TourDTO
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
                Id = r.Id,
                Description = r.Description,
                Name = r.Name,
                IsDeleted = r.IsDeleted,
                TourId = r.TourId,
            };
        }
        public static UserLocationVisitDTO MapUserLocationVisitToDTO(UserLocationVisit userLocationVisit)
        {
            return new UserLocationVisitDTO
            {
                Id = userLocationVisit.Id,
                UserId = userLocationVisit.UserId,
                IsDeleted = userLocationVisit.IsDeleted,
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


        public static TransactionHistoryDTO MapTransactionHistoryToDTO(TransactionHistory transaction)
        {
            return new TransactionHistoryDTO
            {
                Id = transaction.Id,
                UserId = transaction.UserId,
                TourId = transaction.TourId,
                Amount = transaction.Amount,
                Description = transaction.Description,
                Type = transaction.Type,
                Status = transaction.Status,
                IsDeleted = transaction.IsDeleted,
                CreatedAt = transaction.CreatedAt,
                UpdatedAt = transaction.UpdatedAt,
            };
        }

        public static UserTourProgressDTO MapUserTourProgressToDTO(UserTourProgress userTourProgress)
        {
            return new UserTourProgressDTO
            {
                Id = userTourProgress.Id,
                UserId = userTourProgress.UserId,
                TourId = userTourProgress.TourId,
                StartedAt = userTourProgress.StartedAt,
                FinishedAt = userTourProgress.FinishedAt,
                Status = userTourProgress.Status,
                CurrentCheckpointId = userTourProgress.CurrentCheckpointId,
                Score = userTourProgress.Score,
                GroupMode = userTourProgress.GroupMode,
                GroupId = userTourProgress.GroupId,
                CreatedAt = userTourProgress.CreatedAt,
                UpdatedAt = userTourProgress.UpdatedAt,
                IsDeleted = userTourProgress.IsDeleted,
            };
        }

        public static UserFollowDTO MapUserFollowToDTO(UserFollow userFollow)
        {
            return new UserFollowDTO
            {
                Id = userFollow.Id,
                FollowerId = userFollow.FollowerId,
                FollowingId = userFollow.FollowingId,
                CreatedAt = userFollow.CreatedAt,
                UpdatedAt = userFollow.UpdatedAt,
                IsDeleted = userFollow.IsDeleted,
            };
        }

        public static UserAudioTourDTO MapUserAudioTourToDTO(UserAudioTour userAudioTour)
        {
            return new UserAudioTourDTO
            {
                Id = userAudioTour.Id,
                UserId = userAudioTour.UserId,
                TourId = userAudioTour.TourId,
                CreatedAt = userAudioTour.CreatedAt,
                UpdatedAt = userAudioTour.UpdatedAt,
                IsDeleted = userAudioTour.IsDeleted,
            };
        }

        public static CheckpointAudioDTO MapCheckpointAudioToDTO (CheckpointAudio checkpointAudio)
        {
            return new CheckpointAudioDTO
            {
                Id = checkpointAudio.Id,
                CheckpointId = checkpointAudio.CheckpointId,
                AudioCharacterId = checkpointAudio.AudioCharacterId,
                FileUrl = checkpointAudio.FileUrl,
                IsDefault = checkpointAudio.IsDefault,
                Transcript = checkpointAudio.Transcript,
                CreatedAt = checkpointAudio.CreatedAt,
                UpdatedAt = checkpointAudio.UpdatedAt,
                IsDeleted = checkpointAudio.IsDeleted,
            };
        }

        public static SavedTourDTO MapSavedTourToDTO(SavedTour savedTour)
        {
            return new SavedTourDTO
            {
                Id = savedTour.Id,
                TourId = savedTour.TourId,
                UserId = savedTour.UserId,
                PlannedTime = savedTour.PlannedTime,
                SavedAt = savedTour.SavedAt,
            };
        }

        public static AudioCharacterDTO MapAudioCharacterToDTO(AudioCharacter audioCharacter)
        {
            return new AudioCharacterDTO
            {
                Id = audioCharacter.Id,
                Name = audioCharacter.Name,
                Description = audioCharacter.Description,
                AvatarUrl = audioCharacter.AvatarUrl,
                VoiceType = audioCharacter.VoiceType,
                CreatedAt = audioCharacter.CreatedAt,
                UpdatedAt = audioCharacter.UpdatedAt,
                IsDeleted = audioCharacter.IsDeleted,
            };
        }

        public static TourPreferenceDTO MapTourPreferenceToDTO (TourPreference tourPreference)
        {
            return new TourPreferenceDTO
            {
                Id = tourPreference.Id,
                TourId = tourPreference.TourId,
                UserId = tourPreference.UserId,
                PredictedScore = tourPreference.PredictedScore,
                CreatedAt = tourPreference.CreatedAt,
            };
        }

        public static NotificationDTO MapNotificationToDTO(Notification notification)
        {
            return new NotificationDTO
            {
                Id = notification.Id,
                Content = notification.Content,
                UserId = notification.UserId,
                Type = notification.Type,
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt,
            };
        }
        public static PlaySessionDTO MapPlaySessionToDTO(PlaySession playSession)
        {
            return new PlaySessionDTO
            {
                Id = playSession.Id,
                GroupId = playSession.GroupId,
                RouteId = playSession.RouteId,
                UserId = playSession.UserId,
                StartTime = playSession.StartTime,
                EndTime = playSession.EndTime,
            };
        }

        public static PlayResultDTO MapPlayResultToDTO(PlayResult playResult)
        {
            return new PlayResultDTO
            {
                Id = playResult.Id,
                SessionId = playResult.SessionId,
                Score = playResult.Score,
                CompletedAt = playResult.CompletedAt,
            };
        }
        public static GroupDTO MapGroupToDTO(Group group) 
        {
            return new GroupDTO
            {
                Id = group.Id,
                Name = group.Name,
                CreatedAt = group.CreatedAt,
                IsDeleted = group.IsDeleted,
            };
        } 
        public static GroupMemberDTO MapGroupMemberToDTO(GroupMember groupMember)
        {
            return new GroupMemberDTO
            {
                Id = groupMember.Id,
                UserId = groupMember.UserId,
                GroupId = groupMember.GroupId,
                JoinedAt = groupMember.JoinedAt,

            };

        }

        public static VoucherDTO MapVoucherToDTO(Voucher voucher)
        {
            return new VoucherDTO
            {
                Id = voucher.Id,
                Code = voucher.Code,
                Title = voucher.Title,
                Discount = voucher.Discount,
                CreatedAt = voucher.CreatedAt,
                ExpiryDate = voucher.ExpiryDate,
                IsDeleted = voucher.IsDeleted,
            };
        }

        public static UserVoucherDTO MapUserVoucherToDTO(UserVoucher userVoucher)
        {
            return new UserVoucherDTO
            {
                Id = userVoucher.Id,
                UserId = userVoucher.UserId,
                VoucherId = userVoucher.VoucherId,
                UsedAt = userVoucher.UsedAt,
                IsDeleted = userVoucher.IsDeleted,
                
            };
        }

        public static TourCheckpointDTO MapTourCheckpointToDTO(TourCheckpoint tourCheckpoint)
        {
            return new TourCheckpointDTO
            {
                Id = tourCheckpoint.Id,
                Title = tourCheckpoint.Title,
                Description = tourCheckpoint.Description,
                Latitude = tourCheckpoint.Latitude,
                Longitude = tourCheckpoint.Longitude,
                Order = tourCheckpoint.Order,
                RouteId = tourCheckpoint.RouteId,
                TourId = tourCheckpoint.TourId,
                CreatedAt = tourCheckpoint.CreatedAt,
                UpdatedAt = tourCheckpoint.UpdatedAt,
                IsDeleted = tourCheckpoint.IsDeleted,
            };
        }

        public static CheckpointImageDTO MapCheckpointImageToDTO(CheckpointImage req)
        {
            return new CheckpointImageDTO
            {
                Id = req.Id,
                TourCheckpointId = req.TourCheckpointId,
                Description = req.Description,
                ImageUrl = req.ImageUrl,
                CreatedAt = req.CreatedAt,
                UpdatedAt = req.UpdatedAt,
                IsDeleted = req.IsDeleted,
            };
        }

        public static LeaderboardDTO MapLeaderboardToDTO(Leaderboard leaderboard)
        {
            return new LeaderboardDTO
            {
                Id = leaderboard.Id,
                TourId = leaderboard.TourId,
                UserId = leaderboard.UserId,
                Score = leaderboard.Score,
                Rank = leaderboard.Rank,
                CreatedAt = leaderboard.CreatedAt,
                UpdatedAt = leaderboard.UpdatedAt,
                IsDeleted = leaderboard.IsDeleted,
            };
        }

        public static ChatRoomDTO MapChatRoomToDTO (ChatRoom chatRoom)
        {
            return new ChatRoomDTO
            {
                Id = chatRoom.Id,
                Name = chatRoom.Name,
                Type = chatRoom.Type,
                IsActive = chatRoom.IsActive,
                CreatedBy = chatRoom.CreatedBy,
                CreatedAt = chatRoom.CreatedAt,
                UpdatedAt = chatRoom.UpdatedAt,
            };
        }

        public static MessageDTO MapMessageToDTO (Message message)
        {

            return new MessageDTO
            {
                Id = message.Id,
                ChatRoomId = message.ChatRoomId,
                Content = message.Content,
                SenderId = message.SenderId,
                Type = message.Type,
                CreatedAt = message.CreatedAt,
                Status = message.Status,
            };
        }

        public static ChatRoomMemberDTO MapChatRoomMemberToDTO (ChatRoomMember member)
        {
            return new ChatRoomMemberDTO
            {
                Id = member.Id,
                ChatRoomId = member.ChatRoomId,
                UserId = member.UserId,
                Nickname = member.Nickname,
                IsHost = member.IsHost,
                JoinedAt = member.JoinedAt
            };
         }
    }
}