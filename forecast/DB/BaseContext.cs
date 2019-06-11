using MongoDB.Driver;
using ReceivingService.Services;

namespace ReceivingService.DB
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