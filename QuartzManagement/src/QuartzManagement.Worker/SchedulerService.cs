using QuartzManagementCore;

namespace QuartzManagement.Worker
{
    public class SchedulerService : IHostedService
    {
        private readonly ScheduleService _scheduleService;

        public SchedulerService(ScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _scheduleService.StartSchedulerAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
