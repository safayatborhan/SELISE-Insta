using AutoMapper;
using InstaAPI.Commands;
using InstaAPI.Models;
using InstaAPI.Repository.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InstaAPI.Handlers
{
    public class CreatePhotoHandler : IRequestHandler<CreatePhotoCommand, Photo>
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;
        public CreatePhotoHandler(IPhotoRepository photoRepository, IMapper mapper)
        {
            _photoRepository = photoRepository;
            _mapper = mapper;
        }
        public async Task<Photo> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
        {
            var photo = _mapper.Map<Photo>(request);
            await _photoRepository.CreatePhoto(photo);
            return photo;
        }
    }
}
