using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAPI.Models
{
    [BsonIgnoreExtraElements]
    public class PhotoCategory
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("category_name")]
        public string CategoryName { get; set; }
    }
}
