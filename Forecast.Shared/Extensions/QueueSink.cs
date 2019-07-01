using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.PeriodicBatching;

namespace Forecast.Shared.Extensions
{
    public class QueueSink : PeriodicBatchingSink
    {
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2.0);
        private readonly string _collectionName;
        private readonly ITextFormatter _textFormatter;
        public const int DefaultBatchPostingLimit = 50;
        public const string DefaultCollectionName = "log";

        public QueueSink(string host, int batchSizeLimit, TimeSpan period, IFormatProvider formatProvider, string queueName, ITextFormatter textFormatter) : base(batchSizeLimit, period)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException(nameof(host));
            if(string.IsNullOrEmpty(queueName))
                throw new ArgumentNullException(nameof(queueName));
            _textFormatter = textFormatter;
        }
    
        private static IMongoDatabase DatabaseFromMongoUrl(string databaseUrlOrConnStrName)
        {
            if (string.IsNullOrWhiteSpace(databaseUrlOrConnStrName))
                throw new ArgumentNullException(nameof(databaseUrlOrConnStrName));
            MongoUrl url = MongoUrl.Create(databaseUrlOrConnStrName);
            return new MongoClient(url).GetDatabase(url.DatabaseName, (MongoDatabaseSettings) null);
        }

        private IMongoCollection<BsonDocument> GetLogCollection()
        {
            return this._mongoDatabase.GetCollection<BsonDocument>(this._collectionName,
                (MongoCollectionSettings) null);
        }

        protected virtual Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            return Task.WhenAll(this.GetLogCollection().InsertManyAsync((IEnumerable<BsonDocument>) events.GenerateBsonDocuments(this._textFormatter), (InsertManyOptions) null, new CancellationToken()));
        }
    }
}