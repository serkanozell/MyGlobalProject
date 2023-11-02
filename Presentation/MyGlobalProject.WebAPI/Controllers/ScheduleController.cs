using Hangfire;
using Microsoft.AspNetCore.Mvc;
using MyGlobalProject.Infrastructure.Schedule.BackgroundJobs;

namespace MyGlobalProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IBackgroundJobClient _jobClient;

        public ScheduleController(IBackgroundJobClient jobClient)
        {
            _jobClient = jobClient;
        }

        [HttpGet("[action]")]
        public IActionResult TriggerFireAndFogetJob()
        {
            _jobClient.Enqueue(() => FireAndForgetJobs.LogAllActiveCategories());
            //FireAndForgetJobs.LogAllActiveCategories();

            return Ok();
        }
    }
}
