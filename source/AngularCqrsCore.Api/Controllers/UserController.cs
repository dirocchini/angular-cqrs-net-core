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

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }


        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
