using Microsoft.AspNetCore.Mvc;
using QuartzManagementCore;
using QuartzManagementCore.Entity;

namespace QuartzManagement.Worker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly ScheduleService _scheduleService;

        public JobController(ILogger<JobController> logger, ScheduleService scheduleService)
        {
            _logger = logger;
            _scheduleService = scheduleService;
        }

        [HttpPost("http-job")]
        public async Task<bool> AddHttpJob(HttpJobParameter httpJobParameter)
        {
            await _scheduleService.AddHttpJob(httpJobParameter);
            return true;
        }

        [HttpDelete("http-job")]
        public async Task<bool> DeleteHttpJob(HttpJobParameter httpJobParameter)
        {
            return await _scheduleService.DeleteJobAsync(httpJobParameter.JobGroup, httpJobParameter.JobName);
        }
    }
}