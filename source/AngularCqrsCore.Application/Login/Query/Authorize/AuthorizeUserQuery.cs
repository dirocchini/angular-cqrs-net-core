using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            private readonly IApplicationDbContext _applicationDbContext;

            public AuthorizeUserQueryHandler(IUserRepository userRepository, IMapper mapper, IApplicationDbContext applicationDbContext)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _applicationDbContext = applicationDbContext;
            }

            public async Task<AuthorizedUser> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _applicationDbContext.User.FirstOrDefaultAsync(u => u.Login.ToLower().Trim() == request.Login.ToLower().Trim(), cancellationToken);

                if (user == null || request.Password.Trim() != user.Password.Decrypt().Trim())
                    return null;

                var userToReturn = _mapper.Map<AuthorizedUser>(user);

                user.LastActive = DateTime.Now;
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return userToReturn;
            }
        }
    }
}