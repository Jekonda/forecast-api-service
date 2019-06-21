using System.Text;
using System.Threading.Tasks;
using Forecast.DataReceiver.Configuration;
using RabbitMQ.Client;
using Serilog;

namespace Forecast.DataReceiver.Services.Messages
{
    public class RabbitSender : IQueueSender
    {
        private readonly ILogger _logger;
        private readonly IRabbitSettings _rabbitSettings;
        
        public RabbitSender(ILogger logger, IRabbitSettings rabbitSettings)
        {
            _logger = logger;
            _rabbitSettings = rabbitSettings;
        }

        public async Task SendAsync(object message, string queueName = null)
        {
            await Task.Run(() =>
            {
                var factory = new ConnectionFactory {HostName = _rabbitSettings.HostName};
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        queueName = string.IsNullOrEmpty(queueName) ? _rabbitSettings.DefaultQueue : queueName;
                        channel.QueueDeclare(queue: queueName,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
                        var body = Encoding.UTF8.GetBytes(message.ToString());
                        _logger.Debug($"Message to sent: {message}");
                        channel.BasicPublish(exchange: "",
                            routingKey: queueName,
                            basicProperties: null, body);
                    }
                }
            });
        }
    }
}