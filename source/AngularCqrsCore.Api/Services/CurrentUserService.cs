using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;

namespace Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService()
        {
            UserId = 1;
            UserName = "Teste";
        }

        public int UserId { get; }
        public string UserName { get; }
    }
}
