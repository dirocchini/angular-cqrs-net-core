using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dto;
using Api.Helpers;
using Application.Photos.Commands.Create;
using Application.Photos.Queries.Get;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedOps;

namespace Api.Controllers
{
    [Authorize]
    [Route("user/{id}/photos")]
    [ApiController]
    public class PhotoController : BaseController
    {
        private readonly IOptions<CloudinarySettings> _cloudinaryOptions;
        private readonly CreatePhotoCommand _createPhotoCommand;

        public PhotoController(IOptions<CloudinarySettings> cloudinaryOptions, CreatePhotoCommand createPhotoCommand)
        {
            _cloudinaryOptions = cloudinaryOptions;
            _createPhotoCommand = createPhotoCommand;
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int id, IFormFile command)
        {
            _createPhotoCommand.File = Request.Form.Files[0];
            _createPhotoCommand.UserId = id;
            var photo = await Mediator.Send(_createPhotoCommand);
            
            return Ok(photo);
        }

        [HttpGet(Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photo = await Mediator.Send(new GetPhotoQuery() { Id = id });
            return Ok(photo);
        }
    }
}