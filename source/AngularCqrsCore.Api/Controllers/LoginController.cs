using System.Threading.Tasks;
using Application.Login.Query.Authorize;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedOps;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public partial class LoginController : BaseController
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }


        [HttpPost]
        public async Task<IActionResult> Authorize(AuthorizeUserQuery request)
        {
            var user = await Mediator.Send(request);
            if (user == null)
                return Unauthorized();

            return Ok(new
            {
                token = new TokenTools(_config).MakeToken(user.Id, user.Login, user.Roles),
                user
            });
        }
    }
}