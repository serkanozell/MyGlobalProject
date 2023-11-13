using MyGlobalProject.Application.Dto.UserDtos;
using MyGlobalProject.Application.ServiceInterfaces.UserServices;

namespace MyGlobalProject.Infrastructure.Schedule.BackgroundJobs
{
    public class DelayedJobs
    {
        private readonly IUserService _userService;

        public DelayedJobs(IUserService userService)
        {
            _userService = userService;
        }

        public static void SendRegisteredUserToAllAdmins(CreateUserDTO createUserDTO)
        {
            Hangfire.BackgroundJob.Schedule<IUserService>(x => x.SendMailToAllAdmins(createUserDTO), TimeSpan.FromMinutes(1));
        }
    }
}
