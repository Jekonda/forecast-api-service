using MongoDB.Driver;

namespace ReceivingService.Services
{
    public interface IDataCollection<T>
    {
        IMongoCollection<T> GetCollection();
    }
}