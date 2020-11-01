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
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PhotosController(IPhotoRepository photoRepository, IMapper mapper, IMediator mediator)
        {
            _photoRepository = photoRepository;
            _mapper = mapper;
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
            //var photos = _photoRepository.GetPhotos();
            //var photosDto = new List<PhotoDto>();
            //foreach(var photo in photos)
            //{
            //    photosDto.Add(_mapper.Map<PhotoDto>(photo));
            //}
            //return Ok(photosDto);
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

            //var photo = _photoRepository.GetPhoto(photoId);
            //if(photo == null)
            //{
            //    return NotFound();
            //}
            //var photoDto = _mapper.Map<PhotoDto>(photo);
            //return Ok(photoDto);
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
            //if (photoDto == null)
            //{
            //    return BadRequest(ModelState);
            //}
            //var photo = _mapper.Map<Photo>(photoDto);
            //try
            //{
            //    _photoRepository.CreatePhoto(photo);
            //}
            //catch(Exception ex)
            //{
            //    ModelState.AddModelError("", $"Something went wrong when saving this record");
            //    return StatusCode(500, ModelState);
            //}
            //return CreatedAtRoute("GetPhoto", new { photoId = photo.Id }, photo);
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
            //var photo = _mapper.Map<Photo>(photoDto);
            //try
            //{
            //    _photoRepository.UpdatePhoto(photo);
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError("", $"Something went wrong when updating this record");
            //    return StatusCode(500, ModelState);
            //}
            //return NoContent();
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

            if (!_photoRepository.PhotoExists(photoId))
            {
                return NotFound();
            }
            var photo = _photoRepository.GetPhoto(photoId);
            try
            {
                //_photoRepository.DeletePhoto(photo);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Something went wrong when deleting this record");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
