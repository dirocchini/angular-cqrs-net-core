using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Users.Queries.Get
{
    public class GetQuery : IRequest<UserVm>
    {
        public int Id { get; set; }


        public class GetQueryHandler : IRequestHandler<GetQuery, UserVm>
        {
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;

            public GetQueryHandler(IMapper mapper, IUserRepository userRepository)
            {
                _mapper = mapper;
                _userRepository = userRepository;
            }
            public async Task<UserVm> Handle(GetQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(request.Id);

                var userVm = _mapper.Map<UserVm>(user);

                return userVm;
            }
        }
    }
}
