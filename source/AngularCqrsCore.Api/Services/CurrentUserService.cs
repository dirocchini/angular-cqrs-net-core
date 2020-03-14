using Api.Common;
using Application.Common.Interfaces;
using Application.Login.Query.Authorize;
using Microsoft.AspNetCore.Http;

namespace Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService()
        {
            UserId = -1;
            UserName = "Not Logged";
        }

        public int UserId { get; }

        public string UserName { get; }
    }
}
