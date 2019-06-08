using MongoDB.Driver;
using ReceivingService.Models;

namespace ReceivingService.Services
{
    public class EntityCollection<T>: IDataCollection<T>
    {
        private readonly IDataStoreDatabaseSettings _settings;
        
        public EntityCollection(IDataStoreDatabaseSettings settings)
        {
            _settings = settings;
        }

        public IMongoCollection<T> GetCollection()
        {
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            return database.GetCollection<T>(_settings.ForecastCollectionName);
        }
    }
}