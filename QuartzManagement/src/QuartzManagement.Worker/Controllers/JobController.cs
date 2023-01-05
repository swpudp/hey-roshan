using Microsoft.AspNetCore.Mvc;
using QuartzManagement.Core;
using QuartzManagement.Core.Entity;
using QuartzManagement.Core.Request;

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

        /// <summary>
        /// 删除指定的任务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("http-job")]
        public async Task<bool> DeleteHttpJob(OperateJobRequest request)
        {
            return await _scheduleService.DeleteJobAsync(request.JobName, request.JobGroup);
        }

        /// <summary>
        /// 停止指定的任务
        /// </summary>
        /// <param name="request">任务分组</param>
        /// <returns></returns>
        [HttpPost("pauseJob")]
        public async Task<bool> PauseJobAsync(OperateJobRequest request)
        {
            return await _scheduleService.PauseJobAsync(request.JobName, request.JobGroup);
        }

        /// <summary>
        /// 恢复指定的任务
        /// </summary>
        /// <param name="request">任务分组</param>
        /// <returns></returns>
        [HttpPost("resumeJob")]
        public async Task<bool> ResumeJobAsync(OperateJobRequest request)
        {
            return await _scheduleService.ResumeJobAsync(request.JobName, request.JobGroup);
        }
    }
}