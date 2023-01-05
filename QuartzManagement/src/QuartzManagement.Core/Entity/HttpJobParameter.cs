using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzManagement.Core.Entity
{
    public class HttpJobParameter
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 任务分组
        /// </summary>
        public string JobGroup { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTimeOffset BeginTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTimeOffset? EndTime { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string Cron { get; set; }

        /// <summary>
        /// 执行次数（默认无限循环）
        /// </summary>
        public int RunTimes { get; set; }

        /// <summary>
        /// 执行间隔时间，单位秒（如果有Cron，则IntervalSecond失效）
        /// </summary>
        public int IntervalSecond { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
