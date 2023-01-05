using Quartz;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzManagementCore
{
    public class Ext
    {
        public async Task Build()
        {
            // you can have base properties
            var properties = new NameValueCollection();

            // and override values via builder
            IScheduler scheduler = await SchedulerBuilder.Create(properties)
                // default max concurrency is 10
                .UseDefaultThreadPool()
                // this is the default 
                // .WithMisfireThreshold(TimeSpan.FromSeconds(60))
                .UsePersistentStore(x =>
                {
                    // force job data map values to be considered as strings
                    // prevents nasty surprises if object is accidentally serialized and then 
                    // serialization format breaks, defaults to false
                    x.UseProperties = true;
                    x.UseClustering();
                    // there are other SQL providers supported too 
                    x.UseSqlServer("my connection string");
                    // this requires Quartz.Serialization.Json NuGet package
                    x.UseJsonSerializer();
                })
                .BuildScheduler();

            await scheduler.Start();
        }
    }
}
