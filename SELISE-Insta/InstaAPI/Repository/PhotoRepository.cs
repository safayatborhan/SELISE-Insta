using InstaAPI.Models;
using InstaAPI.Repository.IRepository;
using MediatR;
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
        public Task CreatePhoto(Photo photo)
        {
            return _photoCollection.InsertOneAsync(photo);
        }

        public Task DeletePhoto(Photo photo)
        {
            return _photoCollection.DeleteOneAsync(p => p.Id == photo.Id);
        }

        public Task<Photo> GetPhoto(string photoId)
        {
            var photo = _photoCollection.FindAsync<Photo>(p => p.Id == photoId).Result.FirstOrDefaultAsync();
            return photo;
        }

        public Task<List<Photo>> GetPhotos()
        {
            var photos = _photoCollection.FindAsync(p => true).Result.ToListAsync();
            return photos;
        }

        public Task UpdatePhoto(Photo photo)
        {
            return _photoCollection.ReplaceOneAsync(p => p.Id == photo.Id, photo);
        }

        public bool PhotoExists(string id)
        {
            var photo = GetPhoto(id);
            return photo == null ? false : true; 
        }
    }
}
