namespace Forecast.DataReceiver.Configuration
{
    public class RabbitSettings: IRabbitSettings
    {
        public string HostName { get; set; }
        public string QueueName { get; set; }
        public string ExchangeName { get; set; }
    }

    public interface IRabbitSettings
    {
        string HostName { get; set; }
        string QueueName { get; set; }
        string ExchangeName { get; set; }

    }
}