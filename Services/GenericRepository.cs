using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ReceivingService.Services
{
    public class GenericRepository<T> : IRepository<T>
    {
        private readonly IMongoCollection<T> _collection;

        public GenericRepository(IDataCollection<T> collection)
        {
            _collection = collection.GetCollection();
        }

        public async Task Save(T item)
        {
            await _collection.InsertOneAsync(item);
        }

        IEnumerable<T> IRepository<T>.GetAll()
            => _collection.Find(x => true).ToEnumerable();

    }
    
    

}