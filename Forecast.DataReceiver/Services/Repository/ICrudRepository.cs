using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace Forecast.DataReceiver.Services.Repository
{
    public interface ICrudRepository
    {
         Task<T> SaveAsync<T>(T entity);
         Task<List<T>> ToListAsync<T>();
         IMongoQueryable<T> AsQueryable<T>();
         Task<bool> UpdateAsync<T>(T entity, Func<T, bool> filter);

    }
}