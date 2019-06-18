using forecast.Models;
using forecast.Services.Client;
using MongoDB.Driver;

namespace forecast.Services
{
    public class EntityCollection : IDataCollection
    {
        private readonly IDatabaseSettings _settings;
        private readonly MongoClient _client;
        
        public EntityCollection(IDatabaseSettings settings, IClient client)
        {
            _settings = settings;
            _client = client.GetClient(settings.ConnectionString);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            var database = _client.GetDatabase(_settings.DatabaseName);
            return database.GetCollection<T>(typeof(T).Name);
        }
    }
}