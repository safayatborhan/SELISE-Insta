using InstaAPI.Models;
using InstaAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAPI.Repository.IRepository
{
    public interface IPhotoRepository
    {
        ICollection<Photo> GetPhotos();
        Photo GetPhoto(string photoId);
        void CreatePhoto(Photo photo);
        void UpdatePhoto(Photo photo);
        void DeletePhoto(Photo photo);
    }
}
