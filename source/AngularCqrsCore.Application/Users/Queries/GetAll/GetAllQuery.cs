using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Users.Queries.GetAll
{
    public class GetAllQuery : IRequest<List<UserVm>>
    {
        private const int MaxPageSize = 15;

        public int PageNumber { get; set; } = 1;


        private int pageSize { get; set; }
        public int PageSize { get { return PageSize}; set; }




        public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<UserVm>>
        {
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;

            public GetAllQueryHandler(IMapper mapper, IUserRepository userRepository)
            {
                _mapper = mapper;
                _userRepository = userRepository;
            }

            public async Task<List<UserVm>> Handle(GetAllQuery request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetAllAsync();

                var usersDto = _mapper.Map<List<UserVm>>(users);

                return usersDto;
            }
        }
    }
}
