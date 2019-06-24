using MongoDB.Driver;

namespace Forecast.Shared.Db
{
    public interface IClient
    {
        MongoClient GetClient(string connectionString);
    }
}