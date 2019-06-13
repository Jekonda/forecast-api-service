using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReceivingService.Models
{
    public class Forecast
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public decimal Humidity { get; set; }
        public decimal Temp { get; set; }
        
        public DateTimeOffset CreatedOn { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Humidity)}: {Humidity}, {nameof(Temp)}: {Temp}, {nameof(CreatedOn)}: {CreatedOn}";
        }
    }
}