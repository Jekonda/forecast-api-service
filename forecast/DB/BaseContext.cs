using forecast.Services;
using MongoDB.Driver;

namespace forecast.DB
{
    public class BaseContext
    {
        protected readonly IDataCollection Db;

        protected BaseContext(IDataCollection db)
        {
            Db = db;
        }

        public IMongoCollection<T> Set<T>() => Db.GetCollection<T>();
    }
}