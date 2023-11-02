using MyGlobalProject.Application.ServiceInterfaces.CategoryServices;
using MyGlobalProject.Application.ServiceInterfaces.Notification;

namespace MyGlobalProject.Infrastructure.Schedule.BackgroundJobs
{
    public class FireAndForgetJobs
    {
        public static void MailWithHangfire()
        {
            Hangfire.BackgroundJob.Enqueue<IEmailSender>(x => x.SendEmailAsync(new Application.Dto.EmailDtos.EmailDTO
            {
                To = "serkan.fb1994@gmail.com",
                Body = "Hangfire message",
                Subject = "Hangfire test"
            }));
        }
        
        public static void LogAllActiveCategories()
        {
            Hangfire.BackgroundJob.Enqueue<ICategoryService>(x => x.LogAllCategories());
        }
    }
}
