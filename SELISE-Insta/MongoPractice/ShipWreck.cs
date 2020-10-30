using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoPractice
{
    [BsonIgnoreExtraElements]
    public class ShipWreck
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("feature_type")]
        public string FeatureType { get; set; }        
        public string Chart { get; set; }
        [BsonElement("latdec")]
        public double Latitude { get; set; }
        [BsonElement("longdec")]
        public double Longitude { get; set; }
        //[BsonExtraElements]
        //public object[] Bucket { get; set; }
    }
}
