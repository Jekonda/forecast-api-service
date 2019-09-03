using MongoDB.Driver;

namespace Forecast.Shared.Db
{
    public class BaseContext
    {
        protected readonly CollectionFactory CollectionFactory;

        protected BaseContext(CollectionFactory db)
        {
            CollectionFactory = db;
        }

        public IMongoCollection<T> Set<T>() => CollectionFactory.GetCollection<T>();
    }
}