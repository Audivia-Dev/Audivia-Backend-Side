﻿using Audivia.Domain.DTOs;
using Audivia.Domain.Models;
using System.Data;

namespace Audivia.Domain.Commons.Mapper
{
    public static class ModelMapper
    {
        public static UserDTO MapUserToDTO(User user,
            string roleName = "",
            int? followers = null,
            int? following = null,
            int? friends = null)
        {
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.Username,
                FullName = user.FullName,
                Phone = user.Phone,
                AvatarUrl = user.AvatarUrl,
                CoverPhoto = user.CoverPhoto,
                Followers = followers,
                Following = following,
                Friends = friends,
                Bio = user.Bio,
                BalanceWallet = user.BalanceWallet,
                AudioCharacterId = user.AudioCharacterId,
                AutoPlayDistance = user.AutoPlayDistance,
                TravelDistance = user.TravelDistance,
                RoleId = user.RoleId,
                RoleName = roleName,
                ConfirmedEmail = user.ConfirmedEmail,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                IsDeleted = user.IsDeleted,
                BirthDay = user.BirthDay,
                Gender = user.Gender,
                Country = user.Country,
                Job = user.Job
            };
        }

        public static UserShortDTO MapUserToShortDTO(User user)
        {
            return new UserShortDTO
            {
                Id = user.Id,
                UserName = user.Username,
                FullName = user.FullName,
                AvatarUrl = user.AvatarUrl,
            };
        }
        public static TourDTO MapAudioTourToDTO(Tour tour)
        {
            return new TourDTO
            {
                Id = tour.Id,
                Title = tour.Title,
                Location = tour.Location,
                StartLatitude = tour.StartLatitude,
                StartLongitude = tour.StartLongitude,
                Description = tour.Description,
                Price = tour.Price,
                Duration = tour.Duration,
                TypeId = tour.TypeId,
                TourTypeName = tour.TourType != null ? tour.TourType.TourTypeName : null,
                ThumbnailUrl = tour.ThumbnailUrl,
                AvgRating = Math.Round((double)tour.AvgRating, 1),
                RatingCount = tour.RatingCount,
                IsDeleted = tour.IsDeleted,
                CreatedAt = tour.CreatedAt,
                UpdatedAt = tour.UpdatedAt
            };
        }

        public static TourDTO MapTourDetailsToDTO(Tour tour, IEnumerable<TourCheckpoint> checkpoints)
        {
            return new TourDTO
            {
                Id = tour.Id,
                Title = tour.Title,
                Description = tour.Description,
                Location = tour.Location,
                StartLatitude = tour.StartLatitude,
                StartLongitude = tour.StartLongitude,
                Price = tour.Price,
                Duration = tour.Duration,
                TypeId = tour.TypeId,
                TourTypeName = tour.TourType != null ? tour.TourType.TourTypeName : null,
                ThumbnailUrl = tour.ThumbnailUrl,
                UseCustomMap = tour.UseCustomMap,
                CustomMapImages = tour.CustomMapImages,
                AvgRating = Math.Round((double)tour.AvgRating, 1),
                RatingCount = tour.RatingCount,
                Checkpoints = checkpoints.Select(MapTourCheckpointToDTO),
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

        public static PostDTO MapPostToDTO(Post post, User user, int? likes = null, int? comments = null, string? time = null)
        {
            return new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Images = post.Images,
                Location = post.Location,
                Likes = likes,
                Comments = comments,
                Time = time,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt,
                User = MapUserToShortDTO(user),
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

        public static CommentDTO MapCommentToDTO(Comment comment, string? username = null)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt,
                CreatedBy = comment.CreatedBy,
                UserName = username,
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
                TourId = q.TourId,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                IsDeleted = q.IsDeleted,
                QuestionsCount = q.QuestionsCount,
                Image = q.Image,
            };
        }
        public static QuestionDTO MapQuestionToDTO(Question question)
        {
            return new QuestionDTO
            {
                Id = question.Id,
                Text = question.Text,
                ImageUrl = question.ImageUrl,
                QuizId = question.QuizId,
                Points = question.Points,
                Order = question.Order,
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
                Text = a.Text
            };
        }

        public static UserQuestionResponseDTO MapUserQuestionResponseToDTO(UserQuestionResponse userResponse, string? trueAnswerNote = null)
        {
            return new UserQuestionResponseDTO
            {
                QuizId = userResponse.QuizId,
                QuestionId = userResponse.QuestionId,
                AnswerId = userResponse.AnswerId,
                IsCorrect = userResponse.IsCorrect,
                UserId = userResponse.UserId,
                TrueAnswerNote = trueAnswerNote
            };
        }

        public static UserQuestionResponseDTO MapUserQuestionResponseToDTO(UserQuestionResponse userResponse)
        {
            return new UserQuestionResponseDTO
            {
                QuizId = userResponse.QuizId,
                QuestionId = userResponse.QuestionId,
                AnswerId = userResponse.AnswerId,
                IsCorrect = userResponse.IsCorrect,
                UserId = userResponse.UserId
            };
        }

        public static UserQuizResponseDTO MapUserQuizResponseToDTO(UserQuizResponse userQuizResponse)
        {
            return new UserQuizResponseDTO
            {
                Id = userQuizResponse.Id,
                QuizId = userQuizResponse.QuizId,
                CorrectAnswersCount = userQuizResponse.CorrectAnswersCount,
                QuestionAnswers = (userQuizResponse.QuestionAnswers?.Select(MapUserQuestionResponseToDTO)).ToList(),
                IsDone = userQuizResponse.IsDone,
                RespondedAt = userQuizResponse.RespondedAt,
                UserId = userQuizResponse.UserId
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
                AudioCharacterId = transaction.AudioCharacterId,
                Amount = transaction.Amount,
                Description = transaction.Description,
                Type = transaction.Type,
                Status = transaction.Status,
                IsDeleted = transaction.IsDeleted,
                CreatedAt = transaction.CreatedAt,
                UpdatedAt = transaction.UpdatedAt,
                Tour = transaction.Tour,
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
                IsCompleted = userTourProgress.IsCompleted,
                CurrentCheckpointId = userTourProgress.CurrentCheckpointId,
                Score = userTourProgress.Score,
                GroupMode = userTourProgress.GroupMode,
                GroupId = userTourProgress.GroupId
            };
        }

        public static UserTourProgressDTO MapUserTourProgressToDTO(UserTourProgress userTourProgress, IEnumerable<UserCheckpointProgress>? checkpointProgress = null)
        {
            return new UserTourProgressDTO
            {
                Id = userTourProgress.Id,
                UserId = userTourProgress.UserId,
                TourId = userTourProgress.TourId,
                StartedAt = userTourProgress.StartedAt,
                FinishedAt = userTourProgress.FinishedAt,
                IsCompleted = userTourProgress.IsCompleted,
                CurrentCheckpointId = userTourProgress.CurrentCheckpointId,
                Score = userTourProgress.Score,
                GroupMode = userTourProgress.GroupMode,
                GroupId = userTourProgress.GroupId,
                CheckpointProgress = checkpointProgress?.Select(MapUserCheckpointProgressToDTO)
            };
        }

        public static UserCheckpointProgressDTO MapUserCheckpointProgressToDTO(UserCheckpointProgress userCheckpointProgress)
        {
            return new UserCheckpointProgressDTO
            {
                Id = userCheckpointProgress.Id,
                UserTourProgressId = userCheckpointProgress.UserTourProgressId,
                CheckpointAudioId = userCheckpointProgress.CheckpointAudioId,
                TourCheckpointId = userCheckpointProgress.TourCheckpointId,
                IsCompleted = userCheckpointProgress.IsCompleted,
                LastListenedTime = userCheckpointProgress.LastListenedTime,
                ProgressSeconds = userCheckpointProgress.ProgressSeconds,
            };
        }

        public static UserFollowDTO MapUserFollowToDTO(UserFollow userFollow)
        {
            return new UserFollowDTO
            {
                Id = userFollow.Id,
                FollowerId = userFollow.FollowerId,
                FollowingId = userFollow.FollowingId,
                AreFriends = userFollow.AreFriends,
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
                TourCheckpointId = checkpointAudio.TourCheckpointId,
                AudioCharacterId = checkpointAudio.AudioCharacterId,
                FileUrl = checkpointAudio.FileUrl,
                VideoUrl = checkpointAudio.VideoUrl,
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
                Tour = savedTour.Tour,
   
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
                AudioUrl = audioCharacter.AudioUrl,
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
                TourId = notification.TourId,
            };
        }
        public static PlaySessionDTO MapPlaySessionToDTO(PlaySession playSession)
        {
            return new PlaySessionDTO
            {
                Id = playSession.Id,
                TourId = playSession.TourId,
                GroupId = playSession.GroupId,
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
                TourId = tourCheckpoint.TourId,
                Audios = tourCheckpoint.Audios == null ? null : tourCheckpoint.Audios.Select(MapCheckpointAudioToDTO),
                Images = tourCheckpoint.Images == null ? null : tourCheckpoint.Images.Select(MapCheckpointImageToDTO),
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
                Members = chatRoom.Members,
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
                Sender = message.Sender,
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

        public static PaymentTransactionDTO MapPaymentTransactioToDTO(PaymentTransaction paymentTransaction)
        {
            return new PaymentTransactionDTO
            {
                Id = paymentTransaction.Id,
                OrderCode = paymentTransaction.OrderCode,
                UserId = paymentTransaction.UserId,
                Amount = paymentTransaction.Amount,
                CreatedAt = paymentTransaction.CreatedAt,
                Description = paymentTransaction.Description,
                PaymentTime = paymentTransaction.PaymentTime,
                Status = paymentTransaction.Status,

            };
        }
    }
}