using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoPractice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IMongoCollection<ShipWreck> _shipwreckCollection;
        public WeatherForecastController(IMongoClient client)
        {
            //var client = new MongoClient("mongodb+srv://sa:cse@cluster0.oaaet.mongodb.net/<dbname>?retryWrites=true&w=majority");
            var database = client.GetDatabase("sample_geospatial");
            //var collection = database.GetCollection<BsonDocument>("shipwrecks");  //dirty way to get data with a generic collection

            //There are 4 ways for accessing data with mongodb driver
            //MQL : Mongo query language
            //BsonDocument
            //Builders
            //LINQ & Mapping Classes

            _shipwreckCollection = database.GetCollection<ShipWreck>("shipwrecks");
        }

        [HttpGet]
        public IEnumerable<ShipWreck> Get()
        {            
            return _shipwreckCollection.Find(x => x.FeatureType == "Wrecks - Visible").ToList();
        }
    }
}
