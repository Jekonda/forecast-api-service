using System;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Forecast.Shared.Extensions
{
    public static class LoggerConfigurationQueueExtensions
    {
        public static LoggerConfiguration Microservice(
            this LoggerSinkConfiguration loggerConfiguration,
            string host,
            string queueName = "log",
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Verbose,
            int batchPostingLimit = 50,
            TimeSpan period = default,
            ITextFormatter textFormatter = null)
        {
            if (loggerConfiguration == null)
                throw new ArgumentNullException(nameof (loggerConfiguration));
            if (string.IsNullOrWhiteSpace(host))
                throw new ArgumentNullException(nameof (host));
            return loggerConfiguration.Sink((ILogEventSink) new QueueSink(host, batchPostingLimit, period, queueName, textFormatter), restrictedToMinimumLevel);
        }
    }
}