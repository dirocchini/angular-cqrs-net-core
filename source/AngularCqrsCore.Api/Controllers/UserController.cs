using System.Threading.Tasks;
using Application.Users.Commands.Create;
using Application.Users.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        // GET: api/User
        [HttpGet]
        public async Task<UsersVm> Get()
        {
            var users = await Mediator.Send(new GetAllQuery());
            return users;
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

    }
}
