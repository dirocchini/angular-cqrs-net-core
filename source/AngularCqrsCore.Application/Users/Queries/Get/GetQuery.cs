using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.Get
{
    public class GetQuery : IRequest<UserVm>
    {
        public int Id { get; set; }
        public int CurrentUserId { get; set; }


        public class GetQueryHandler : IRequestHandler<GetQuery, UserVm>
        {
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;
            private readonly IApplicationDbContext _applicationDbContext;

            public GetQueryHandler(IMapper mapper, IUserRepository userRepository, IApplicationDbContext applicationDbContext)
            {
                _mapper = mapper;
                _userRepository = userRepository;
                _applicationDbContext = applicationDbContext;
            }
            public async Task<UserVm> Handle(GetQuery request, CancellationToken cancellationToken)
            {
                var query = _applicationDbContext.User.Include(p => p.Photos).AsQueryable();

                if(request.Id == request.CurrentUserId)
                    query = query.IgnoreQueryFilters();

                var user = await query.FirstOrDefaultAsync(u => u.Id == request.Id);

                var userVm = _mapper.Map<UserVm>(user);

                return userVm;
            }
        }
    }
}
