using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using forecast.Attributes;
using forecast.Models;
using forecast.Services.Client;
using MongoDB.Bson;
using MongoDB.Driver;

namespace forecast.Services
{
    public class EntityCollection : IDataCollection
    {
        private readonly MongoClient _client;
        private readonly IDatabaseSettings _settings;
        private IMongoDatabase Database => _client.GetDatabase(_settings.DatabaseName);

        public EntityCollection(IDatabaseSettings settings, IClient client)
        {
            _settings = settings;
            _client = client.GetClient(settings.ConnectionString);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            var collectionName = typeof(T).GetCustomAttributes<CollectionName>().FirstOrDefault()?.Name ??
                                 typeof(T).Name;
            if (!CollectionExistsAsync(collectionName).Result)
            {
                Database.CreateCollection(collectionName);
            }

            return Database.GetCollection<T>(collectionName);
        }

        public async Task<bool> CollectionExistsAsync(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collections = await Database.ListCollectionsAsync(new ListCollectionsOptions {Filter = filter});
            return await collections.AnyAsync();
        }
    }
}