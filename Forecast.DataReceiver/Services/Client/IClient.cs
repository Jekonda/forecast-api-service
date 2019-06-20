using MongoDB.Driver;

namespace forecast.Services.Client
{
    public interface IClient
    {
        MongoClient GetClient(string connectionString);
    }
}