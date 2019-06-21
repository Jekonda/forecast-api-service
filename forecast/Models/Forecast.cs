using System;
using forecast.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace forecast.Models
{
    [CollectionName("Forecast")]
    public class Forecast
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public decimal Humidity { get; set; }
        public decimal Temp { get; set; }
        public decimal Pressure { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedOn { get; set; }
        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Humidity)}: {Humidity}, {nameof(Temp)}: {Temp}, {nameof(Pressure)}: {Pressure}, {nameof(CreatedOn)}: {CreatedOn}";
        }
    }
}