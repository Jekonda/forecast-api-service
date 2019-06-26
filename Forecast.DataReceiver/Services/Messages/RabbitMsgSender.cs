using System.Text;
using System.Threading.Tasks;
using Forecast.Shared.Configuration;
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

        public async Task SendAsync(object message)
        {
            await Task.Run(() =>
            {
                var factory = new ConnectionFactory {HostName = _rabbitSettings.HostName};
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: _rabbitSettings.ExchangeName, type: "fanout");
                    var body = Encoding.UTF8.GetBytes(message.ToString());
                    _logger.Information($"Message to sent: {message}");
                    channel.BasicPublish(exchange: _rabbitSettings.ExchangeName,
                        routingKey: "",
                        basicProperties: null, body);
                }
            });
        }
    }
}