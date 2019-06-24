using Forecast.Shared.Db;
using MongoDB.Driver;

namespace Forecast.DataReceiver.DB
{
    public class AppDbContext: BaseContext
    {
        public AppDbContext(IDataCollection db) : base(db)
        {
        
        }
        public IMongoCollection<Models.Forecast> Forecast => Db.GetCollection<Models.Forecast>();

    }
}