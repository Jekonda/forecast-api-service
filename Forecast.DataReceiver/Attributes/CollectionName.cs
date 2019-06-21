using System;

namespace Forecast.DataReceiver.Attributes
{
    public class CollectionName: Attribute
    {
        public String Name { get; set; }

        public CollectionName(string collectionName)
        {
            if(string.IsNullOrEmpty(collectionName))
              throw  new ArgumentNullException(nameof(collectionName));
            Name = collectionName;
        }
    }
}