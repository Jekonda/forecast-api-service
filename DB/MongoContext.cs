using System.ComponentModel;
using MongoDB.Driver;
using ReceivingService.Models;
using ReceivingService.Services;

namespace ReceivingService.DB
{
    public class MongoContext
    {
        private IMongoCollection<Forecast> _forecast;
        private IMongoCollection<Logs> _logs;

        private readonly IDataCollection<Forecast> _forecastCollection;
        private readonly IDataCollection<Logs> _logsCollection;

        public MongoContext(IDataCollection<Forecast> forecastCollection, IDataCollection<Logs> logsCollection)
        {
            _forecastCollection = forecastCollection;
            _logsCollection = logsCollection;
        }

        public IMongoCollection<Forecast> Forecast
        {
            get
            {
                if (_forecast != null)
                    return _forecast;
                _forecast = _forecastCollection.GetCollection();
                return _forecast;
            }
        }

        public IMongoCollection<Logs> Logs
        {
            get
            {
                if (_logs != null)
                    return _logs;
                _logs = _logsCollection.GetCollection();
                return _logs;
            }
        }
    }
}