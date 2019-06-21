using System.Threading;
using System.Threading.Tasks;
using forecast.Configuration;
using Forecast.SocketWriter.Notification;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace Forecast.SocketWriter.HostedService
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
            _channel.QueueDeclare(_rabbitSettings.DefaultQueue, true, false, false, null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                // received message  
                var content = System.Text.Encoding.UTF8.GetString(ea.Body);
                await _notifyService.SendNotificationAsync(content);
                _channel.BasicAck(ea.DeliveryTag, false);
            };
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