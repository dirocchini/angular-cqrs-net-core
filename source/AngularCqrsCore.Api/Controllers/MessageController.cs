using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Messages.Queries.GetMessage;
using Application.Messages.Commands.CreateMessage;
using Application.Messages.Queries.GetMessagesForUser;
using Api.Helpers;
using Application.Messages.Queries.GetMessageThread;

namespace Api.Controllers
{
    [Authorize]
    [Route("user/{userId}/[controller]")]
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
        [HttpGet]
        public async Task<IActionResult> GetMessages(int userId, [FromQuery]GetMessagesForUserQuery request)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            request.CurrentUserId = userId;

            var messagesPagination = await Mediator.Send(request);

            Response.AddPagination(messagesPagination.CurrentPage, messagesPagination.ItemsPerPage, messagesPagination.TotalItems, messagesPagination.TotalPages);
            return Ok(messagesPagination.Messages);
        }

        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessagesThread(int userId, int recipientId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messages = await Mediator.Send(new GetMessageThreadQuery() { SenderId = userId, RecipientId = recipientId });

            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, CreateMessageCommand request)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            request.SenderId = userId;

            var message = await Mediator.Send(request);

            if (message != null)
                return CreatedAtRoute("GetMessage", new { userId = userId, messageId = message.Id }, message);

            return BadRequest();
        }
    }
}