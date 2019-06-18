using System.Threading.Tasks;
using MongoDB.Driver;

namespace forecast.Services
{
    public interface IDataCollection
    {
        IMongoCollection<T> GetCollection<T>();
        Task<bool> CollectionExistsAsync(string collectionName);
    }
}