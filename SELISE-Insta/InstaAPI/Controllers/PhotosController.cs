using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InstaAPI.Models;
using InstaAPI.Models.Dtos;
using InstaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;

        public PhotosController(IPhotoRepository photoRepository, IMapper mapper)
        {
            _photoRepository = photoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPhotos()
        {
            var photos = _photoRepository.GetPhotos();
            var photosDto = new List<PhotoDto>();
            foreach(var photo in photos)
            {
                photosDto.Add(_mapper.Map<PhotoDto>(photo));
            }
            return Ok(photosDto);
        }

        [HttpGet("{photoId}", Name = "GetPhoto")]
        public IActionResult GetPhoto(string photoId)
        {
            var photo = _photoRepository.GetPhoto(photoId);
            if(photo == null)
            {
                return NotFound();
            }
            var photoDto = _mapper.Map<PhotoDto>(photo);
            return Ok(photoDto);
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] PhotoDto photoDto)
        {
            if(photoDto == null)
            {
                return BadRequest(ModelState);
            }
            var photo = _mapper.Map<Photo>(photoDto);
            try
            {
                _photoRepository.CreatePhoto(photo);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", $"Something went wrong when saving this record");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetPhoto", new { photoId = photo.Id }, photo);
        }

        [HttpPatch("{photoId}", Name = "UpdatePhoto")]
        public IActionResult UpdatePhoto(string photoId, [FromBody] PhotoDto photoDto)
        {
            if (photoDto == null || photoId != photoDto.Id)
            {
                return BadRequest(ModelState);
            }
            var photo = _mapper.Map<Photo>(photoDto);
            try
            {
                _photoRepository.UpdatePhoto(photo);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Something went wrong when updating this record");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{photoId}", Name = "DeletePhoto")]
        public IActionResult DeletePhoto(string photoId)
        {
            if (!_photoRepository.PhotoExists(photoId))
            {
                return NotFound();
            }
            var photo = _photoRepository.GetPhoto(photoId);
            try
            {
                _photoRepository.DeletePhoto(photo);
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
