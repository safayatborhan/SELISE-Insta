using InstaAPI.Models.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAPI.Queries
{
    public class GetAllPhotosQuery : IRequest<List<PhotoDto>>
    {
    }
}
