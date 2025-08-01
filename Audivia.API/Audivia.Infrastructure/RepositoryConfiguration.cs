﻿using Audivia.Infrastructure.Interface;
using Audivia.Infrastructure.Repositories.Implemetation;
using Audivia.Infrastructure.Repositories.Interface;
using Audivia.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Audivia.Infrastructure
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddRepository(this IServiceCollection service)
        {
            service.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<ITourTypeRepository, TourTypeRepository>();
            service.AddScoped<ITourRepository, TourRepository>();
            
            service.AddScoped<IQuizFieldRepository, QuizFieldRepository>();
            service.AddScoped<IRoleRepository, RoleRepository>();
            
            service.AddScoped<IPostRepository, PostRepository>();
            service.AddScoped<ITourReviewRepository, TourReviewRepository>();
            service.AddScoped<ICommentRepository, CommentRepository>();
            service.AddScoped<IReactionRepository, ReactionRepository>();
            
            service.AddScoped<IQuizRepository, QuizRepository>();
            service.AddScoped<IQuestionRepository, QuestionRepository>();
            service.AddScoped<IAnswerRepository, AnswerRepository>();
            
            service.AddScoped<IUserCurrentLocationRepository, UserCurrentLocationRepository>();
            service.AddScoped<IUserLocationVisitRepository, UserLocationVisitRepository>();
            service.AddScoped<IUserQuizResponseRepository, UserQuizResponseRepository>();
            
            service.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>(); 
            service.AddScoped<IUserAudioTourRepository, UserAudioTourRepository>();
            service.AddScoped<IUserFollowRepository, UserFollowRepository>();
            service.AddScoped<IUserTourProgressRepository, UserTourProgressRepository>();
            service.AddScoped<ICheckpointAudioRepository, CheckpointAudioRepository>();
            service.AddScoped<ISavedTourRepository, SavedTourRepository>();
            service.AddScoped<INotificationRepository, NotificationRepository>();
            service.AddScoped<ITourPreferenceRepository, TourPreferenceRepository>();
            service.AddScoped<IAudioCharacterRepository, AudioCharacterRepository>();

            service.AddScoped<IPlaySessionRepository, PlaySessionRepository>();
            service.AddScoped<IPlayResultRepository, PlayResultRepository>();
            service.AddScoped<IGroupMemberRepository, GroupMemberRepository>();
            service.AddScoped<IGroupRepository, GroupRepository>();
            service.AddScoped<IVoucherRepository, VoucherRepository>();
            service.AddScoped<IUserVoucherRepository, UserVoucherRepository>(); 
            service.AddScoped<ILeaderboardRepository, LeaderboardRepository>();
            service.AddScoped<ITourCheckpointRepository, TourCheckpointRepository>();
            service.AddScoped<ICheckpointImageRepository, CheckpointImageRepository>();

            service.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            service.AddScoped<IChatRoomMemberRepository, ChatRoomMemberRepository>();
            service.AddScoped<IMessageRepository, MessageRepository>();
            service.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();

            service.AddScoped<IChatBotMessageRepository, ChatBotMessageRepository>();
            service.AddScoped<IChatBotSessionRepository, ChatBotSessionRepository>();

            service.AddScoped<IUserCheckpointProgressRepository, UserCheckpointProgressRepository>();
           
            return service;
        }
    }
}
