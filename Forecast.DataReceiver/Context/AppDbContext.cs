using Forecast.DataReceiver.Models;
using Forecast.Shared.Db;
using MongoDB.Driver;

namespace Forecast.DataReceiver.Context
{
    public class AppDbContext: BaseContext
    {
        public AppDbContext(CollectionFactory factory) : base(factory) {}
        
        public IMongoCollection<Prediction> Forecast => CollectionFactory.GetCollection<Prediction>();
    }
}