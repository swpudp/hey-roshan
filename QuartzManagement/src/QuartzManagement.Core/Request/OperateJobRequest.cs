using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzManagement.Core.Request
{
    public class OperateJobRequest : BaseRequest
    {
        /// <summary>
        /// 任务分组
        /// </summary>
        public string JobGroup { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }

        public override string CreateSignature()
        {
            throw new NotImplementedException();
        }
    }
}
