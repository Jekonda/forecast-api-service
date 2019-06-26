using System.Threading.Tasks;
using MongoDB.Driver;

namespace Forecast.Shared.Db
{
    public interface IDataCollection
    {
        IMongoCollection<T> GetCollection<T>();
        Task<bool> CollectionExistsAsync(string collectionName);
    }
}