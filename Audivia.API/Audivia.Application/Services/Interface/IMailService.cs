using Audivia.Domain.ModelRequests.Mail;

namespace Audivia.Application.Services.Interface
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest request);
    }
}
