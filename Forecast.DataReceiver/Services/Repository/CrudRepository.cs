using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forecast.DataReceiver.DB;
using MongoDB.Driver;

namespace Forecast.DataReceiver.Services.Repository
{
    public class CrudRepository : ICrudRepository
    {
        private readonly AppDbContext _appDbContext;

        public CrudRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<T> Save<T>(T item)
        {
            await _appDbContext.Set<T>().InsertOneAsync(item);
            return item;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _appDbContext.Set<T>().Find(x => true).ToEnumerable();
        }

        public async Task<bool> Update<T>(T entity, Func<T, bool> filter)
        {
            var actionResult =
                await _appDbContext.Set<T>().ReplaceOneAsync(f => filter(f), entity, new UpdateOptions());
            return actionResult.IsAcknowledged
                   && actionResult.ModifiedCount > 0;
        }
    }
}