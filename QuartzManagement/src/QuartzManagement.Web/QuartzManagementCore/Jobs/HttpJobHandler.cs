using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzManagementCore.Jobs
{
    public class HttpJobHandler : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss.ffff") + "：" + context.Scheduler.SchedulerInstanceId);
            return Task.CompletedTask;
        }
    }
}
