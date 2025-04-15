
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


            return service;
        }
    }
}
