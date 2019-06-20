namespace forecast.Configuration
{
    public class RabbitSettings: IRabbitSettings
    {
        public string HostName { get; set; }
        public string DefaultQueue { get; set; }
    }

    public interface IRabbitSettings
    {
        string HostName { get; set; }
        string DefaultQueue { get; set; }
    }
}