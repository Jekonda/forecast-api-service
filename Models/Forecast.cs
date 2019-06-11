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
        public int PlantHumidity { get; set; }
        public string PlantStatus { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Humidity)}: {Humidity}, {nameof(Temp)}: {Temp}, {nameof(PlantHumidity)}: {PlantHumidity}, {nameof(PlantStatus)}: {PlantStatus}, {nameof(CreatedOn)}: {CreatedOn}";
        }
    }
}