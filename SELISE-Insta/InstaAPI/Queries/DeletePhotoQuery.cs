using InstaAPI.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAPI.Queries
{
    public class DeletePhotoQuery : IRequest<Photo>
    {
        public string Id { get; set; }
        public DeletePhotoQuery(string id)
        {
            Id = id;
        }
    }
}
