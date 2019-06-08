using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReceivingService.Models
{
    public class Logs
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("Level")]
        public string Level { get; set; }
        
        [BsonElement("LogBody")]
        public string LogBody { get; set; }
        
        [BsonElement("Time")]
        public DateTime Time { get; set; }
    }
}