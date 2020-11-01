using InstaAPI.Models;
using InstaAPI.Models.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAPI.Repository.IRepository
{
    public interface IPhotoRepository
    {
        Task<List<Photo>> GetPhotos();
        Task<Photo> GetPhoto(string photoId);
        Task CreatePhoto(Photo photo);
        Task UpdatePhoto(Photo photo);
        Task DeletePhoto(Photo photo);
        bool PhotoExists(string id);
    }
}
