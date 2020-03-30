using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dto;
using Api.Helpers;
using Application.Photos.Commands.Create;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedOps;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/user/{id}/photos")]
    [ApiController]
    public class PhotoController : BaseController
    {
        private readonly IOptions<CloudinarySettings> _cloudinaryOptions;

        public PhotoController(IOptions<CloudinarySettings> cloudinaryOptions)
        {
            _cloudinaryOptions = cloudinaryOptions;
        }


        public async Task<IActionResult> AddPhotoForUser(int id, CreatePhotoCommand command)
        {
            if (await Mediator.Send(command))
                return Ok();

            return BadRequest();
        }
    }
}