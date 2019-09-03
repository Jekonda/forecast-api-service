using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forecast.DataReceiver.Context;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Forecast.DataReceiver.Services.Repository
{
    public class CrudRepository : ICrudRepository
    {
        private readonly AppDbContext _appDbContext;
        
        public CrudRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<T> SaveAsync<T>(T item)
        {
            await _appDbContext.Set<T>().InsertOneAsync(item);
            return item;
        }

        public IMongoQueryable<T> AsQueryable<T>()
        {
            return _appDbContext.Set<T>().AsQueryable();
        }
        
        public async Task<List<T>> ToListAsync<T>()
        {
            return await _appDbContext.Set<T>().Find(x => true).ToListAsync();
        }

        public async Task<bool> UpdateAsync<T>(T entity, Func<T, bool> filter)
        {
            var actionResult =
                await _appDbContext.Set<T>().ReplaceOneAsync(f => filter(f), entity, new UpdateOptions());
            return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
        }
    }
}