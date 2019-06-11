using System;
using MongoDB.Driver;

namespace ReceivingService.Services.Client
{
    public class MongoDbClient: IClient
    {
        public MongoClient GetClient(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            var client = new MongoClient(connectionString);
            if(client == null)
                throw new MongoClientException(nameof(connectionString));
            return client;
        }
    }
}