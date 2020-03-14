using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Login.Query.Authorize;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        [HttpPost]
        public async Task<bool> Authorize(AuthorizeUserQuery request)
        {
            var user = await Mediator.Send(request);
            return user != null;
        }
    }
}