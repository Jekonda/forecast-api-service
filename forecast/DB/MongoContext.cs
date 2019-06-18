using forecast.Models;
using forecast.Services;
using MongoDB.Driver;

namespace forecast.DB
{
    public class MongoContext: BaseContext
    {
        public MongoContext(IDataCollection db) : base(db)
        {
        }
        
        public IMongoCollection<Forecast> Forecast => Db.GetCollection<Forecast>();
    }
}