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
    public class UpdatePhotoHandler : IRequestHandler<UpdatePhotoCommand, Photo>
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;
        public UpdatePhotoHandler(IPhotoRepository photoRepository, IMapper mapper)
        {
            _photoRepository = photoRepository;
            _mapper = mapper;
        }

        public async Task<Photo> Handle(UpdatePhotoCommand request, CancellationToken cancellationToken)
        {
            var photo = _mapper.Map<Photo>(request);
            await _photoRepository.UpdatePhoto(photo);
            return photo;
        }
    }
}
