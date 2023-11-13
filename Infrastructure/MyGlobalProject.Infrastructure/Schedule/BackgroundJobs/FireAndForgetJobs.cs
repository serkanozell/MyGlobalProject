using MyGlobalProject.Application.ServiceInterfaces.CategoryServices;

namespace MyGlobalProject.Infrastructure.Schedule.BackgroundJobs
{
    public class FireAndForgetJobs
    {
        public static void LogAllActiveCategories()
        {
            Hangfire.BackgroundJob.Enqueue<ICategoryService>(x => x.LogAllCategories());
        }
    }
}
