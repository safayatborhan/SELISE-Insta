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
    public class GetPhotoHandler : IRequestHandler<GetPhotoQuery, PhotoDto>
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;
        public GetPhotoHandler(IPhotoRepository photoRepository, IMapper mapper)
        {
            _photoRepository = photoRepository;
            _mapper = mapper;
        }
        public async Task<PhotoDto> Handle(GetPhotoQuery request, CancellationToken cancellationToken)
        {
            var photo = await _photoRepository.GetPhoto(request.Id);
            if (photo == null)
            {
                return null;
            }
            var photoDto = _mapper.Map<PhotoDto>(photo);
            return photoDto;
        }
    }
}
