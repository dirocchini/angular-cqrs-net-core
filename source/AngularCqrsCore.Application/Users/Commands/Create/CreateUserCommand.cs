using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using SharedOps;

namespace Application.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public CreateUserCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var passConfig = PasswordUtil.CreatePasswordHash(request.Password);

                var user = new User
                {
                    Name = request.Name, 
                    Email = request.Email, 
                    Login = request.Login, 
                    Password = request.Password,
                    PasswordSalt = passConfig.passwordSalt,
                    PasswordHash = passConfig.passwordHash
                };

                await _applicationDbContext.Users.AddAsync(user, cancellationToken);
                return await _applicationDbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}