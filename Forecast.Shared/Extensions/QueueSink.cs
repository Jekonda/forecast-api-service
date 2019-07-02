using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.PeriodicBatching;

namespace Forecast.Shared.Extensions
{
    public class QueueSink : PeriodicBatchingSink
    {
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2.0);
        private readonly ITextFormatter _textFormatter;
        public const int DefaultBatchPostingLimit = 50;
        private readonly string _host;
        private readonly string _queueName;


        public QueueSink(string host, int batchSizeLimit, TimeSpan period, string queueName,
            ITextFormatter textFormatter) : base(batchSizeLimit, period)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException(nameof(host));
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentNullException(nameof(queueName));
            _host = host;
            _queueName = queueName;
            _textFormatter = textFormatter;
        }


        private Task PublishLogs(IEnumerable<LogEvent> events)
        {
            return Task.Run(() =>
            {
                var factory = new ConnectionFactory {HostName = _host};
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    foreach (var logEvent in events)
                    {
                        channel.ExchangeDeclare(exchange: _queueName, type: "fanout");
                        var body = Encoding.UTF8.GetBytes(logEvent.ToString());
                        channel.BasicPublish(exchange: _queueName, routingKey: "", basicProperties: null, body);
                    }
                }
            });
        }

        protected virtual Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            return Task.WhenAny(PublishLogs(events));
        }
    }
}