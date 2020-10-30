using InstaAPI.Models;
using InstaAPI.Repository.IRepository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAPI.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private IMongoCollection<Photo> _photoCollection;
        public PhotoRepository(ISeliseInstaDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _photoCollection = database.GetCollection<Photo>(settings.PhotosCollectionName);
        }
        public void CreatePhoto(Photo photo)
        {
            _photoCollection.InsertOne(photo);
        }

        public void DeletePhoto(Photo photo)
        {
            _photoCollection.DeleteOne(p => p.Id == photo.Id);
        }

        public Photo GetPhoto(string photoId)
        {
            var photo = _photoCollection.Find<Photo>(p => p.Id == photoId).FirstOrDefault();
            return photo;
        }

        public ICollection<Photo> GetPhotos()
        {
            var photos = _photoCollection.Find(p => true).ToList();
            return photos;
        }

        public void UpdatePhoto(Photo photo)
        {
            _photoCollection.ReplaceOne(p => p.Id == photo.Id, photo);
        }

        public bool PhotoExists(string id)
        {
            var photo = GetPhoto(id);
            return photo == null ? false : true; 
        }
    }
}
