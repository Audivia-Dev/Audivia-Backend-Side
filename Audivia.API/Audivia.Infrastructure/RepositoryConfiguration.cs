using Audivia.Infrastructure.Interface;
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
            service.AddScoped<IAudioTourRepository, AudioTourRepository>();
            service.AddScoped<IQuizFieldRepository, QuizFieldRepository>();
            service.AddScoped<IRoleRepository, RoleRepository>();
            service.AddScoped<IPostRepository, PostRepository>();
            service.AddScoped<ITourReviewRepository, TourReviewRepository>();
            service.AddScoped<ICommentRepository, CommentRepository>();
            service.AddScoped<IReactionRepository, ReactionRepository>();
            service.AddScoped<IQuizRepository, QuizRepository>();
            service.AddScoped<IQuestionRepository, QuestionRepository>();
            service.AddScoped<IAnswerRepository, AnswerRepository>();

            return service;
        }
    }
}
