using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest<bool>, IMapFrom<User>
    {
        public int  Id { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }



        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
        {
            private readonly IApplicationDbContext _applicationContext;
            private readonly IMapper _mapper;

            public UpdateUserCommandHandler(IApplicationDbContext applicationContext, IMapper mapper)
            {
                _applicationContext = applicationContext;
                _mapper = mapper;
            }

            public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var token = ClaimTypes.NameIdentifier;
                var userFromToken = _applicationContext.Users.FirstOrDefault(u => u.Id == int.Parse(token));

                if(request.Id != userFromToken.Id)
                    throw new UnauthorizedAccessException();

                var user = await _applicationContext.Users.FirstOrDefaultAsync(u => u.Id == request.Id);

                if (user == null) return false;

                user = _mapper.Map(request, user);

                await _applicationContext.SaveChangesAsync(cancellationToken);

                return true;
            }

     
        }
    }
}
