using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
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
            private readonly IMapper _mapper;

            public AuthorizeUserQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<AuthorizedUser> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _userRepository.GetByLoginAsync(request.Login);

                    if (user == null) return null;

                    return request.Password.Trim() != user.Password.Decrypt().Trim()
                        ? null 
                        : _mapper.Map<AuthorizedUser>(user);
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
