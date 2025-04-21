
using Audivia.API.Middlewares;
using Audivia.Application;
using Audivia.Domain.Commons.Mail;
using Audivia.Infrastructure;
using Audivia.Infrastructure.Data;
using MongoDB.Driver;

namespace Audivia.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<MongoDbService>();

            builder.Services.AddSingleton<IMongoDatabase>(sp =>
            {
                var mongoService = sp.GetRequiredService<MongoDbService>();
                return mongoService.Database!;
            });

            builder.Services.AddRepository();

            builder.Services.AddService();

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

            builder.Services.AddScoped<ErrorHandlingMiddleware>();

            var app = builder.Build();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
