using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Photo
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("user_id")]
        public string UserId { get; set; }
        [BsonElement("image_url")]
        public string ImageUrl { get; set; }
        [BsonElement("photo_description")]
        public string PhotoDescription { get; set; }
        [BsonElement("photo_categories")]
        public List<PhotoCategory> PhotoCategories { get; set; }
        [BsonElement("photo_upload_time")]
        public DateTime PhotoUploadTime { get; set; }
    }
}
