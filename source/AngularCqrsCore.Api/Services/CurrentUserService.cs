using Application.Common.Interfaces;

namespace Api.Services
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
