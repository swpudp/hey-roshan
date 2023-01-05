using QuartzManagement.Core;

namespace QuartzManagement.Worker
{
    public class ScheduleHostedService : IHostedService
    {
        private readonly ScheduleService _scheduleService;

        public ScheduleHostedService(ScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _scheduleService.StartSchedulerAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduleService.ShutdownSchedulerAsync();
        }
    }
}
