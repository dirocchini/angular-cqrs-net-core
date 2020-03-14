using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using SharedOps;

namespace Application.Login.Query.Authorize
{
    public class AuthorizeUserQuery : IRequest<AuthorizedUser>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public class AuthorizeUserQueryHandler : IRequestHandler<AuthorizeUserQuery, AuthorizedUser>
        {
            private readonly IUserRepository _userRepository;

            public AuthorizeUserQueryHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<AuthorizedUser> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _userRepository.GetByLoginAsync(request.Login);

                    if (user == null) return null;

                    return !PasswordUtil.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt) 
                        ? null 
                        : new AuthorizedUser() {Id = user.Id, Name = user.Name, Login = user.Login};
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
