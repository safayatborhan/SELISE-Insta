using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAPI.Models.Dtos
{
    public class PhotoCategoryDto
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
    }
}
