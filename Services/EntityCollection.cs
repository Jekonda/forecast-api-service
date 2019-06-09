using System;
using System.ComponentModel;
using System.Linq;
using MongoDB.Driver;
using ReceivingService.Models;

namespace ReceivingService.Services
{
    public class EntityCollection<T> : IDataCollection<T>
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
            var propertyInfos = _settings.GetType().GetProperties().FirstOrDefault(c => c.Name.Contains(typeof(T).Name));
            if (propertyInfos == null) throw new ArgumentException(nameof(T));
            var collectionName = (string)propertyInfos.GetValue(_settings);
            return database.GetCollection<T>(collectionName);
        }
    }
}