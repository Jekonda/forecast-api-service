using MongoDB.Driver;
using ReceivingService.Models;

namespace ReceivingService.Service
{
    public class MongoRepository : IRepository<Forecast>
    {

        private readonly IMongoCollection<Forecast> _forecast;

        public void Save(Forecast item)
        {
                
        }
    }
}