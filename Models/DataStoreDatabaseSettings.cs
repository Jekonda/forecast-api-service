namespace ReceivingService.Models
{
    public class DataStoreDatabaseSettings : IDataStoreDatabaseSettings
    {
        public string ConnectionString { get ; set ; }
        public string DatabaseName { get; set ; }
    }

    public interface IDataStoreDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}