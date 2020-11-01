using AutoMapper;
using InstaAPI.Models.Dtos;
using InstaAPI.Queries;
using InstaAPI.Repository.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InstaAPI.Handlers
{
    public class GetPhotosHandler : IRequestHandler<GetAllPhotosQuery, List<PhotoDto>>
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;
        public GetPhotosHandler(IPhotoRepository photoRepository, IMapper mapper)
        {
            _photoRepository = photoRepository;
            _mapper = mapper;
        }
        public async Task<List<PhotoDto>> Handle(GetAllPhotosQuery request, CancellationToken cancellationToken)
        {
            var photos = await _photoRepository.GetPhotos();
            var photosDto = new List<PhotoDto>();
            foreach (var photo in photos)
            {
                photosDto.Add(_mapper.Map<PhotoDto>(photo));
            }
            return photosDto;
        }
    }
}
