using System;
using System.Reflection;
using System.Threading.Tasks;
using Application.Users.Commands.Create;
using Application.Users.Commands.Delete;
using Application.Users.Commands.Update;
using Application.Users.Queries.Get;
using Application.Users.Queries.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedOps;

namespace Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IConfiguration _config;

        public UserController(IConfiguration config )
        {
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await Mediator.Send(new GetAllQuery());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var users = await Mediator.Send(new GetQuery { Id = id });
            return Ok(users);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(DeleteUserCommand command)
        {
            try
            {
                if(await Mediator.Send(command))
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserCommand command) //TODO - USAR COMO EXEMPLO PARA VALIDAR TOKEN
        {
            try
            {
                var ret = await Mediator.Send(command);
                if (ret)
                    return NoContent();

                throw new Exception($"Update user {id} failed on save");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
