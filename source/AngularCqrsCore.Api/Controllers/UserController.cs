using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Helpers;
using Application.Users.Commands.Create;
using Application.Users.Commands.Delete;
using Application.Users.Commands.Likes.AddLike;
using Application.Users.Commands.Update;
using Application.Users.Queries.Get;
using Application.Users.Queries.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    
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
        public async Task<IActionResult> Get([FromQuery]GetAllQuery request)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            request.CurrentUserId = currentUserId;


            var userPagination = await Mediator.Send(request);

            Response.AddPagination(userPagination.CurrentPage, userPagination.ItemsPerPage, userPagination.TotalItems, userPagination.TotalPages);
            return Ok(userPagination.Users);
        }

        [HttpGet("{id}", Name="GetUserById")]
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
                return CreatedAtRoute("GetUserById", new { Controller = "User", id = user }, user);
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

        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> LikeUser(int id, int recipientId)
        {
            string ret = await Mediator.Send(new CreateUserLikeCommand(id, recipientId));

            if(string.IsNullOrEmpty(ret))
                return Ok();

            return BadRequest(ret);
        }
    }
}
