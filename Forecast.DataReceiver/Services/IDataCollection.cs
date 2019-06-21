using System.Threading.Tasks;
using MongoDB.Driver;

namespace Forecast.DataReceiver.Services
{
    public interface IDataCollection
    {
        IMongoCollection<T> GetCollection<T>();
        Task<bool> CollectionExistsAsync(string collectionName);
    }
}