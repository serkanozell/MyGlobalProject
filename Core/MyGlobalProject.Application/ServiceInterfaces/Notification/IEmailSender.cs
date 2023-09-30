using MyGlobalProject.Application.Dto.EmailDtos;

namespace MyGlobalProject.Application.ServiceInterfaces.Notification
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailDTO emailDTO);
    }
}
