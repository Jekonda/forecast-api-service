using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using forecast.DB;
using MongoDB.Driver;

namespace forecast.Services.Repository
{
    public class CrudRepository : ICrudRepository
    {
        private readonly MongoContext _mongoContext;

        public CrudRepository(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<T> Save<T>(T item)
        {
            await _mongoContext.Set<T>().InsertOneAsync(item);
            return item;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _mongoContext.Set<T>().Find(x => true).ToEnumerable();
        }

        public async Task<bool> Update<T>(T entity, Func<T, bool> filter)
        {
            var actionResult =
                await _mongoContext.Set<T>().ReplaceOneAsync(f => filter(f), entity, new UpdateOptions());
            return actionResult.IsAcknowledged
                   && actionResult.ModifiedCount > 0;
        }
    }
}