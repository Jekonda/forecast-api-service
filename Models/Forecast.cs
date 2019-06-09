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
        [BsonElement("Humidity")]
        public decimal Humidity { get; set; }
        [BsonElement("Temp")]
        public decimal Temp { get; set; }
        [BsonElement("PlantHumidity")]
        public int PlantHumidity { get; set; }
        
        [BsonElement("PlantStatus")]
        public string PlantStatus { get; set; }
        
        [BsonElement("Time")]
        public DateTime Time { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Humidity)}: {Humidity}, {nameof(Temp)}: {Temp}, {nameof(PlantHumidity)}: {PlantHumidity}, {nameof(PlantStatus)}: {PlantStatus}, {nameof(Time)}: {Time}";
        }
    }
}