using MongoDB.Driver;

namespace ReceivingService.Services.Client
{
    public interface IClient
    {
        MongoClient GetClient(string connectionString);
    }
}