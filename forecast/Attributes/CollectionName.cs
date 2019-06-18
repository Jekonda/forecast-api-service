using System;

namespace forecast.Attributes
{
    public class CollectionName: Attribute
    {
        public String Name { get; set; }

        public CollectionName(string collectionName)
        {
            Name = collectionName;
        }
    }
}