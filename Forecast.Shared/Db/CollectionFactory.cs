using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Forecast.Shared.Attributes;
using Forecast.Shared.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Forecast.Shared.Db
{
    public class CollectionFactory
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public CollectionFactory(IDatabaseSettings settings, IClient client)
        {
            _client = client.GetClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            var collectionName = typeof(T).GetCustomAttributes<CollectionName>().FirstOrDefault()?.Name ??
                                 typeof(T).Name;
            return _database.GetCollection<T>(collectionName);
        }
    }
}