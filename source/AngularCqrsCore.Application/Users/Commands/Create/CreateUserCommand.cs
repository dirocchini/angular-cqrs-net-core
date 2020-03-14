using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

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
                var passConfig = CreatePasswordHash(request.Password);

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

            private (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string requestPassword)
            {
                using var hmac = new System.Security.Cryptography.HMACSHA512();
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestPassword));

                return (passwordSalt, passwordHash);
            }
        }
    }
}