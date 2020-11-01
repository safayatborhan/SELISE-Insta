using AutoMapper;
using InstaAPI.Models;
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
    public class DeletePhotoHandler : IRequestHandler<DeletePhotoQuery, Photo>
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;
        public DeletePhotoHandler(IPhotoRepository photoRepository, IMapper mapper)
        {
            _photoRepository = photoRepository;
            _mapper = mapper;
        }

        public async Task<Photo> Handle(DeletePhotoQuery request, CancellationToken cancellationToken)
        {
            var photo = await _photoRepository.GetPhoto(request.Id);
            await _photoRepository.DeletePhoto(photo);
            return photo;
        }
    }
}
