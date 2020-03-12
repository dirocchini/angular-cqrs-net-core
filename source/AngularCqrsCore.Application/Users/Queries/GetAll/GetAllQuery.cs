using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetAll
{
    public class GetAllQuery : IRequest<UsersVm>
    {
        public class GetAllQueryHandler : IRequestHandler<GetAllQuery, UsersVm>
        {
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;

            public GetAllQueryHandler(IMapper mapper, IUserRepository userRepository)
            {
                _mapper = mapper;
                _userRepository = userRepository;
            }

            public async Task<UsersVm> Handle(GetAllQuery request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetAllAsync();

                var usersDto = _mapper.Map<List<UserDto>>(users);

                return new UsersVm(usersDto);
            }
        }
    }
}
