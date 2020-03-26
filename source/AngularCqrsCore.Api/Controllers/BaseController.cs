using Api.Common;
using Application.Login.Query.Authorize;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Controllers
{
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected AuthorizedUser UserSession
        {
            get => HttpContext.Session.GetObjectFromJson<AuthorizedUser>("User");
            set => HttpContext.Session.SetObjectAsJson("User", value);
        }

        public BaseController()
        {
        }
    }
}
