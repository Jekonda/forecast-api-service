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
        
        public string Level { get; set; }
        
        public string LogBody { get; set; }
        
        public DateTimeOffset CreatedOn { get; set; } = DateTime.Now;
    }
}