using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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



    }
}