using System.Threading;
using System.Threading.Tasks;
using Forecast.DataReceiver.Configuration;
using Forecast.SocketService.Notification;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace Forecast.SocketService.HostedService
{
    public class ConsumerHostedService : BackgroundService
    {
        private readonly INotifyService _notifyService;
        private readonly ILogger _logger;
        private IConnection _connection;
        private IModel _channel;
        private readonly IRabbitSettings _rabbitSettings;

        public ConsumerHostedService(INotifyService notifyService, ILogger logger,
            IRabbitSettings rabbitSettings)
        {
            _notifyService = notifyService;
            _logger = logger;
            _rabbitSettings = rabbitSettings;
            InitRabbitMq();
        }

        private void InitRabbitMq()
        {
            var factory = new ConnectionFactory {HostName = _rabbitSettings.HostName};
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: _rabbitSettings.ExchangeName, type: "fanout");
            _channel.QueueDeclare(_rabbitSettings.QueueName, false, false, true, null);
            _channel.QueueBind(_rabbitSettings.QueueName,
                exchange: _rabbitSettings.ExchangeName,
                routingKey: "");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            _logger.Information($"Execute async");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // received message  
                var content = System.Text.Encoding.UTF8.GetString(ea.Body);
                _logger.Information($"Message received: {content}");
                _channel.BasicAck(ea.DeliveryTag, false);
                //await _notifyService.SendNotificationAsync(content);
            };
            _channel.BasicConsume(_rabbitSettings.QueueName, false, consumer);
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}