using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InstaAPI.Commands;
using InstaAPI.Models;
using InstaAPI.Models.Dtos;
using InstaAPI.Queries;
using InstaAPI.Repository.IRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PhotosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PhotosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets list of photos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PhotoDto>))]
        public async Task<IActionResult> GetPhotos()
        {
            var query = new GetAllPhotosQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets individual photo
        /// </summary>
        /// <param name="photoId">The id of photo</param>
        /// <returns></returns>
        [HttpGet("{photoId}", Name = "GetPhoto")]
        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(PhotoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPhoto(string photoId)
        {
            var query = new GetPhotoQuery(photoId);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult) Ok(result) : NotFound();
        }

        /// <summary>
        /// Saves a single new photo and required information
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatePhotoCommand))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePhoto([FromBody] CreatePhotoCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtRoute("GetPhoto", new { photoId = result.Id }, result);
        }

        /// <summary>
        /// Updates a single photo information
        /// </summary>
        /// <param name="photoId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("{photoId}", Name = "UpdatePhoto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePhoto(string photoId, [FromBody] UpdatePhotoCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null || photoId != result.Id)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes single photo
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        [HttpDelete("{photoId}", Name = "DeletePhoto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePhoto(string photoId)
        {
            var query = new DeletePhotoQuery(photoId);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)NoContent() : NotFound();
        }
    }
}
