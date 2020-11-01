using AutoMapper;
using InstaAPI.Commands;
using InstaAPI.Models;
using InstaAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAPI.InstaMapper
{
    public class InstaMappings : Profile
    {
        public InstaMappings()
        {
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<PhotoCategory, PhotoCategoryDto>().ReverseMap();
            CreateMap<Photo, CreatePhotoCommand>().ReverseMap();
            CreateMap<Photo, UpdatePhotoCommand>().ReverseMap();
        }
    }
}
