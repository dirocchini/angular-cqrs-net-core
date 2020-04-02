using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dto;
using Api.Helpers;
using Application.Photos.Commands.Create;
using Application.Photos.Commands.SetMain;
using Application.Photos.Commands.Update;
using Application.Photos.Queries.Get;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedOps;

namespace Api.Controllers
{
    [Authorize]
    [Route("user/{userId}/photos")]
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
        public async Task<IActionResult> AddPhotoForUser(int userId, IFormFile command)
        {
            _createPhotoCommand.File = Request.Form.Files[0];
            _createPhotoCommand.UserId = userId;
            var photo = await Mediator.Send(_createPhotoCommand);

            return Ok(photo);
        }

        [HttpGet(Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int userId)
        {
            var photo = await Mediator.Send(new GetPhotoQuery() {Id = userId });
            return Ok(photo);
        }

        //[HttpPost("{id}")]
        //public async Task<IActionResult> UpdatePhoto(int userId, UpdatePhotoCommand request)
        //{
        //    return Ok(await Mediator.Send(request));
        //}


        [HttpPost("{photoId}/setmain")]
        public async Task<IActionResult> SetMainPhoto(int userId, int photoId)
        {
            return Ok( await Mediator.Send(new SetMainPhotoCommand(){PhotoId = photoId, UserId = userId}));
        }


    }
}