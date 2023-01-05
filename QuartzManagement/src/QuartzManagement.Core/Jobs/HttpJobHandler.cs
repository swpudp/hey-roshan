using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzManagement.Core.Jobs
{
    [DisallowConcurrentExecution]
    public class HttpJobHandler : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + "：" + context.Scheduler.SchedulerInstanceId + ",seqNumber:" + context.JobDetail.JobDataMap["seqNumber"]);
            return Task.CompletedTask;
        }
    }
}
