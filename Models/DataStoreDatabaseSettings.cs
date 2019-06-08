namespace ReceivingService.Models
{
    public class DataStoreDatabaseSettings : IDataStoreDatabaseSettings
    {
        public string LogsCollectionName { get; set; }
        public string ForecastCollectionName { get; set; }
        public string ConnectionString { get ; set ; }
        public string DatabaseName { get; set ; }
    }

    public interface IDataStoreDatabaseSettings
    {
        string LogsCollectionName { get; set; }
        string ForecastCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}