using MongoDB.Driver;

namespace Forecast.DataReceiver.Services.Client
{
    public interface IClient
    {
        MongoClient GetClient(string connectionString);
    }
}