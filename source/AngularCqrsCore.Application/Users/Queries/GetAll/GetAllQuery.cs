using System.Collections.Generic;
using System.Linq;
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
    public class GetAllQuery : IRequest<UserVMPagination>
    {
        private const int MaxPageSize = 6;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }

        public string Gender { get; set; }
        public int Age { get; set; }

        public int CurrentUserId { get; set; }

        public class GetAllQueryHandler : IRequestHandler<GetAllQuery, UserVMPagination>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _applicationContext;

            public GetAllQueryHandler(IMapper mapper, IApplicationDbContext applicationContext)
            {
                _mapper = mapper;
                _applicationContext = applicationContext;
            }

            public async Task<UserVMPagination> Handle(GetAllQuery request, CancellationToken cancellationToken)
            {
                if (request.PageSize > MaxPageSize || request.PageSize == 0)
                    request.PageSize = MaxPageSize;

                var loggedUser = await _applicationContext.Users.FirstOrDefaultAsync(u => u.Id == request.CurrentUserId);

                if (string.IsNullOrEmpty(request.Gender))
                {
                    request.Gender = "male";

                    if (loggedUser.Gender == "male")
                        request.Gender = "female";
                }

                var users = _applicationContext.Users.Include(u => u.Photos).AsQueryable();
                users = users.Where(u => u.Id != request.CurrentUserId);
                users = users.Where(u => u.Gender.ToLower().Trim() == request.Gender.ToLower().Trim());

                var pagedList = await PagedList<User>.CreateAsync(users, request.PageNumber, request.PageSize);

                var usersVm = _mapper.Map<IEnumerable<UserVm>>(pagedList);
                var ret = new UserVMPagination(pagedList.CurrentPage, pagedList.TotalPages, pagedList.PageSize, pagedList.TotalCount, usersVm);

                return ret;
            }
        }
    }
}
