using System;
using System.Reflection;
using System.Threading.Tasks;
using Application.Users.Commands.Create;
using Application.Users.Commands.Delete;
using Application.Users.Queries.Get;
using Application.Users.Queries.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await Mediator.Send(new GetAllQuery());
            return Ok(users);
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(GetQuery request)
        {
            var users = await Mediator.Send(request);
            return Ok(users);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(DeleteUserCommand request)
        {
            try
            {
                if(await Mediator.Send(request))
                    return Ok();

                return BadRequest("Usuário não encontrado");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddNewUser(CreateUserCommand command)
        {
            try
            {
                var user = await Mediator.Send(command);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
