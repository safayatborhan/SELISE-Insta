using InstaAPI.Models;
using InstaAPI.Models.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAPI.Commands
{
    public class UpdatePhotoCommand : IRequest<Photo>
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ImageUrl { get; set; }
        public string PhotoDescription { get; set; }
        public List<PhotoCategoryDto> PhotoCategories { get; set; }
        public string PhotoUploadTime { get; set; }
    }
}
