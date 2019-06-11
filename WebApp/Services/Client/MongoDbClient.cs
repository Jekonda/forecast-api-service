using System;
using MongoDB.Driver;

namespace ReceivingService.Services.Client
{
    public class MongoDbClient: IClient
    {
        private MongoClient _mongoClient;
        
        public MongoClient GetClient(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            if(_mongoClient == null)
                _mongoClient = new MongoClient(connectionString);
            if(_mongoClient == null)
                throw new MongoClientException(nameof(connectionString));
            return _mongoClient;
        }
    }
}