using forecast.Models;
using forecast.Services;
using MongoDB.Driver;

namespace forecast.DB
{
    public class AppDbContext: BaseContext
    {
        public AppDbContext(IDataCollection db) : base(db)
        {
        }
        public IMongoCollection<Forecast> Forecast => Db.GetCollection<Forecast>();
    }
}