using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using SharedOps;

namespace Api.Filters
{
    public class AuthorizationFilter : IActionFilter
    {
        private readonly IConfiguration _config;

        public AuthorizationFilter(IConfiguration config)
        {
            _config = config;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("command"))
            {
                var command = context.ActionArguments["command"];
                var id = context.ActionArguments["id"];

                if (command is CheckUserValidation)
                {
                    var token2 = context.HttpContext.Request.Headers["Authorization"];
                    var token = context.HttpContext.Request.Headers["token"];
                    var userIdFromToken = new TokenTools(_config).DecodeToken(token);

                    (command as CheckUserValidation).TokenUserId = int.Parse(userIdFromToken);
                    (command as CheckUserValidation).Id = Convert.ToInt16((id));
                }
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}