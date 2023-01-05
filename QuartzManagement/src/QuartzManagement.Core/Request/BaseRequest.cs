using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzManagement.Core.Request
{
    public abstract class BaseRequest
    {
        public string Timestamp { get; set; }

        public string Sign { get; set; }

        public abstract string CreateSignature();
    }
}
