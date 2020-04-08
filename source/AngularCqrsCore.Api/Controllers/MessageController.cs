using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Application.Messages.Queries.GetMessage;
using Application.Messages.Commands.CreateMessage;

namespace Api.Controllers
{
    [Authorize]
    [Route("users/{userId}/[controller]")]
    [ApiController]
    public class MessageController : BaseController
    {

        [HttpGet("{messageId}", Name = "GetMessage")]
        public async Task<IActionResult> Get(int userId, int messageId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var message = await Mediator.Send(new GetMessageQuery() { MessageId = messageId });

            if (message == null)
                return NotFound();

            return Ok(message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, CreateMessageCommand request)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            request.SenderId = userId;

            string ret =await Mediator.Send(request);

            if (string.IsNullOrEmpty(ret))
                return Ok();

            return BadRequest(ret);
        }
    }

   
}