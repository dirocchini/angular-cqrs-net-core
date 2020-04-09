using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

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
            //if (context.ActionArguments.ContainsKey("command"))
            //{
            //    var command = context.ActionArguments["command"];
            //    var id = context.ActionArguments["id"];

            //    if (command is CheckUserValidation)
            //    {
            //        var token = context.HttpContext.Request.Headers["Authorization"];
            //        var userIdFromToken = new TokenTools(_config).DecodeToken(token);

            //        (command as CheckUserValidation).TokenUserId = int.Parse(userIdFromToken);
            //        (command as CheckUserValidation).Id = Convert.ToInt16((id));
            //    }
            //}
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}