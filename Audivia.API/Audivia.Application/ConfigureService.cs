
using Audivia.Application.Services.Implemetation;
using Audivia.Application.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Audivia.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection AddService(this IServiceCollection service)
        {
          
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<ITourTypeService, TourTypeService>();
            service.AddScoped<IAudioTourService, AudioTourService>();
            service.AddScoped<IQuizFieldService, QuizFieldService>();
            service.AddScoped<IRoleService, RoleService>();
            service.AddScoped<IPostService, PostService>();
            service.AddScoped<ICommentService, CommentService>();
            service.AddScoped<IReactionService, ReactionService>();
            service.AddScoped<ITourReviewService, TourReviewService>();
            service.AddScoped<IQuizService, QuizService>();
            service.AddScoped<IQuestionService, QuestionService>();
            service.AddScoped<IAnswerService, AnswerService>();
            service.AddScoped<IRouteService, RouteService>();
            service.AddScoped<IUserResponseService, UserResponseService>();
            service.AddScoped<IUserCurrentLocationService, UserCurrentLocationService>();
            service.AddScoped<IUserLocationVisitService, UserLocationVisitService>();
            return service;
        }
    }
}
