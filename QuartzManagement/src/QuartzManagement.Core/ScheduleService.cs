using Microsoft.Extensions.Options;
using Quartz;
using QuartzManagement.Core.Entity;
using QuartzManagement.Core.Jobs;
using System.Collections.Specialized;

namespace QuartzManagement.Core
{
    /// <summary>
    /// 调度管理
    /// </summary>
    public sealed class ScheduleService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        public ScheduleService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        public async Task AddHttpJob(HttpJobParameter httpJobParameter)
        {
            IScheduler scheduler = await _schedulerFactory.GetScheduler();
            //检查任务是否已存在
            var jobKey = new JobKey(httpJobParameter.JobName, httpJobParameter.JobGroup);
            if (await scheduler.CheckExists(jobKey))
            {
                throw new Exception("task existed");
            }
            IJobConfigurator jobConfigurator = JobBuilder.Create<HttpJobHandler>();
            //http请求配置
            var httpDir = new Dictionary<string, string>()
            {
                //{ Constant.EndAt, entity.EndTime.ToString()},
                //{ Constant.JobTypeEnum, ((int)entity.JobType).ToString()},
                //{ Constant.MAILMESSAGE, ((int)entity.MailMessage).ToString()},
                ["seqNumber"] = Guid.NewGuid().ToString("N")
            };
            IJobDetail jobDetail = jobConfigurator.SetJobData(new JobDataMap(httpDir)).WithDescription(httpJobParameter.Description).WithIdentity(httpJobParameter.JobName, httpJobParameter.JobGroup).Build();
            // 创建触发器
            ITrigger trigger = CreateCronTrigger(httpJobParameter);
            await scheduler.ScheduleJob(jobDetail, trigger);
        }

        /// <summary>
        /// 创建类型Simple的触发器
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private ITrigger CreateSimpleTrigger(HttpJobParameter entity)
        {
            //作业触发器
            if (entity.RunTimes > 0)
            {
                return TriggerBuilder.Create()
               .WithIdentity(entity.JobName, entity.JobGroup)
               .StartAt(entity.BeginTime)//开始时间
                                         //.EndAt(entity.EndTime)//结束数据
               .WithSimpleSchedule(x =>
               {
                   x.WithIntervalInSeconds(entity.IntervalSecond)//执行时间间隔，单位秒
                        .WithRepeatCount(entity.RunTimes)//执行次数、默认从0开始
                        .WithMisfireHandlingInstructionFireNow();
               })
               .ForJob(entity.JobName, entity.JobGroup)//作业名称
               .Build();
            }
            else
            {
                return TriggerBuilder.Create()
               .WithIdentity(entity.JobName, entity.JobGroup)
               .StartAt(entity.BeginTime)//开始时间
                                         //.EndAt(entity.EndTime)//结束数据
               .WithSimpleSchedule(x =>
               {
                   x.WithIntervalInSeconds(entity.IntervalSecond)//执行时间间隔，单位秒
                        .RepeatForever()//无限循环
                        .WithMisfireHandlingInstructionFireNow();
               })
               .ForJob(entity.JobName, entity.JobGroup)//作业名称
               .Build();
            }

        }

        /// <summary>
        /// 创建类型Cron的触发器
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private ITrigger CreateCronTrigger(HttpJobParameter entity)
        {
            // 作业触发器
            return TriggerBuilder.Create()

                   .WithIdentity(entity.JobName, entity.JobGroup)
                   .StartAt(entity.BeginTime)//开始时间
                                             //.EndAt(entity.EndTime)//结束时间
                   .WithCronSchedule(entity.Cron, cronScheduleBuilder => cronScheduleBuilder.WithMisfireHandlingInstructionFireAndProceed())//指定cron表达式                  
                   .ForJob(entity.JobName, entity.JobGroup)//作业名称
                   .Build();
        }

        /// <summary>
        /// 删除指定的任务
        /// </summary>
        /// <param name="jobGroup">任务分组</param>
        /// <param name="jobName">任务名称</param>
        /// <returns></returns>
        public async Task<bool> DeleteJobAsync(string jobName, string jobGroup)
        {
            IScheduler scheduler = await _schedulerFactory.GetScheduler();
            return await scheduler.DeleteJob(new JobKey(jobName, jobGroup));
        }

        /// <summary>
        /// 停止指定的任务
        /// </summary>
        /// <param name="jobGroup">任务分组</param>
        /// <param name="jobName">任务名称</param>
        /// <returns></returns>
        public async Task<bool> PauseJobAsync(string jobName, string jobGroup)
        {
            IScheduler scheduler = await _schedulerFactory.GetScheduler();
            await scheduler.PauseJob(new JobKey(jobName, jobGroup));
            return true;
        }

        /// <summary>
        /// 恢复指定的任务
        /// </summary>
        /// <param name="jobGroup">任务分组</param>
        /// <param name="jobName">任务名称</param>
        /// <returns></returns>
        public async Task<bool> ResumeJobAsync(string jobName, string jobGroup)
        {
            IScheduler scheduler = await _schedulerFactory.GetScheduler();
            await scheduler.ResumeJob(new JobKey(jobName, jobGroup));
            return true;
        }

        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <returns></returns>
        public async Task StartSchedulerAsync()
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            await scheduler.Start();
        }

        /// <summary>
        /// 关闭调度器
        /// </summary>
        /// <returns></returns>
        public async Task ShutdownSchedulerAsync()
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            if (scheduler.IsShutdown)
            {
                return;
            }
            await scheduler.Shutdown();
        }
    }
}
