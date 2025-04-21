
using Audivia.Application.Services.Implemetation;
using Audivia.Application.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Audivia.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection AddService(this IServiceCollection service)
        {
          
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IRoleService, RoleService>();

            service.AddScoped<ITourTypeService, TourTypeService>();
            service.AddScoped<ITourService, TourService>();

            service.AddScoped<IPostService, PostService>();
            service.AddScoped<ICommentService, CommentService>();
            service.AddScoped<IReactionService, ReactionService>();
            service.AddScoped<ITourReviewService, TourReviewService>();

            service.AddScoped<IQuizFieldService, QuizFieldService>();
            service.AddScoped<IQuizService, QuizService>();
            service.AddScoped<IQuestionService, QuestionService>();
            service.AddScoped<IAnswerService, AnswerService>();
            service.AddScoped<IRouteService, RouteService>();
            service.AddScoped<IUserResponseService, UserResponseService>();
            service.AddScoped<IUserCurrentLocationService, UserCurrentLocationService>();
            service.AddScoped<IUserLocationVisitService, UserLocationVisitService>();
            service.AddScoped<ITransactionHistoryService, TransactionHistoryService>();
            service.AddScoped<IUserAudioTourService, UserAudioTourService>();
            service.AddScoped<IUserFollowService, UserFollowService>();
            service.AddScoped<IUserTourProgressService, UserTourProgressService>();

            service.AddScoped<IAudioCharacterService, AudioCharacterService>();
            service.AddScoped<ITourPreferenceService, TourPreferenceService>();
            service.AddScoped<INotificationService, NotificationService>();
            service.AddScoped<ISavedTourService, SavedTourService>();
            service.AddScoped<ICheckpointAudioService, CheckpointAudioService>();
            
            service.AddScoped<IGroupService, GroupService>();
            service.AddScoped<IGroupMemberService, GroupMemberService>();
            service.AddScoped<IPlaySessionService, PlaySessionService>();
            service.AddScoped<IPlayResultService, PlayResultService>();
            service.AddScoped<IVoucherService, VoucherService>();
            service.AddScoped<IUserVoucherService, UserVoucherService>();
            service.AddScoped<ITourCheckpointService, TourCheckpointService>();
            service.AddScoped<ICheckpointImageService, CheckpointImageService>();
            service.AddScoped<ILeaderboardService, LeaderboardService>();

            service.AddScoped<IChatRoomService, ChatRoomService>();
            service.AddScoped<IChatRoomMemberService, ChatRoomMemberService>();
            service.AddScoped<IMessageService, MessageService>();
            return service;
        }
    }
}
