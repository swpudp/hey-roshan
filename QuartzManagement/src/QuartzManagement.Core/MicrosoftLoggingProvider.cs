using Microsoft.Extensions.Logging;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzManagement.Core
{
    public class MicrosoftLoggingProvider : ILogProvider
    {
        private readonly ILoggerFactory loggerFactory;

        public MicrosoftLoggingProvider(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        public Logger GetLogger(string name)
        {
            ILogger logger = loggerFactory.CreateLogger(name);
            return delegate (Quartz.Logging.LogLevel level, Func<string> func, Exception exception, object[] parameters)
            {
                if (func != null)
                {
                    string message = func();
                    switch (level)
                    {
                        case Quartz.Logging.LogLevel.Info:
                            logger.LogInformation(exception, message, parameters);
                            break;
                        case Quartz.Logging.LogLevel.Debug:
                            logger.LogDebug(exception, message, parameters);
                            break;
                        case Quartz.Logging.LogLevel.Error:
                        case Quartz.Logging.LogLevel.Fatal:
                            logger.LogError(exception, message, parameters);
                            break;
                        case Quartz.Logging.LogLevel.Trace:
                            logger.LogTrace(exception, message, parameters);
                            break;
                        case Quartz.Logging.LogLevel.Warn:
                            logger.LogWarning(exception, message, parameters);
                            break;
                    }
                }

                return true;
            };
        }

        public IDisposable? OpenNestedContext(string message)
        {
            return null;
        }

        public IDisposable? OpenMappedContext(string key, object value, bool destructure = false)
        {
            return null;
        }
    }
}
