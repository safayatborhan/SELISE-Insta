using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAPI.Models
{
    public class SeliseInstaDatabaseSettings : ISeliseInstaDatabaseSettings
    {
        public string PhotosCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISeliseInstaDatabaseSettings
    {
        string PhotosCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
