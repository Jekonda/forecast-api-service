using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forecast.DataReceiver.Services.Repository
{
    public interface ICrudRepository
    {
         Task<T> Save<T>(T entity);
         IEnumerable<T> GetAll<T>();
         Task<bool> Update<T>(T entity, Func<T, bool> filter);

    }
}