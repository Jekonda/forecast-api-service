using MongoDB.Driver;

namespace forecast.Services
{
    public interface IDataCollection
    {
        IMongoCollection<T> GetCollection<T>();
    }
}