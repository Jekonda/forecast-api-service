using MongoDB.Driver;

namespace ReceivingService.Services
{
    public interface IDataCollection
    {
        IMongoCollection<T> GetCollection<T>();
    }
}