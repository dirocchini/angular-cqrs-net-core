using System.Threading.Tasks;
using Api.Common;
using Application.Login.Query.Authorize;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedOps;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
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

            return Ok(new { token = new TokenTools(_config).MakeToken(user.Id, user.Login) });
        }
    }
}