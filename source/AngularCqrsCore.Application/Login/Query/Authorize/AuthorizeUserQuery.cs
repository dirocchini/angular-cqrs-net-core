using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;

            public AuthorizeUserQueryHandler(IUserRepository userRepository, IMapper mapper, IApplicationDbContext applicationDbContext, UserManager<User> userManager, SignInManager<User> signInManager)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _applicationDbContext = applicationDbContext;
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<AuthorizedUser> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.Login);
                user.Photos = _applicationDbContext.Photos.Where(p => p.UserId == user.Id).ToList();
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                    return _mapper.Map<AuthorizedUser>(user);

                return null;
            }
        }
    }
}