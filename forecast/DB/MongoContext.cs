using MongoDB.Driver;
using ReceivingService.Models;
using ReceivingService.Services;

namespace ReceivingService.DB
{
    public class MongoContext: BaseContext
    {
        public MongoContext(IDataCollection db) : base(db)
        {
        }
        
        public IMongoCollection<Forecast> Forecast => Db.GetCollection<Forecast>();
    }
}