namespace ReceivingService.Models
{
    public class DataStoreDatabaseSettings : IDatastoreDatabaseSettings
    {
        public string ForecastCollectionName { get; set; }
        public string ConnectionString { get ; set ; }
        public string DatabaseName { get; set ; }
    }

    public interface IDatastoreDatabaseSettings
    {
        string ForecastCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}