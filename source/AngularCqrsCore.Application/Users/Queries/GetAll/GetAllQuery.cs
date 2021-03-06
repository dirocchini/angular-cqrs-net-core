﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetAll
{
    public class GetAllQuery : IRequest<UserVMPagination>
    {
        public int CurrentUserId { get; set; }
        public int Age { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 99;

        private const int MaxPageSize = 6;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }

        public string Gender { get; set; }
        public string OrderBy { get; set; }

        public bool Likees { get; set; } = false;
        public bool Likers { get; set; } = false;






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

                var loggedUser = await _applicationContext.User.FirstOrDefaultAsync(u => u.Id == request.CurrentUserId);

                if (string.IsNullOrEmpty(request.Gender))
                {
                    request.Gender = "male";

                    if (loggedUser.Gender == "male")
                        request.Gender = "female";
                }

                var users = _applicationContext.User.Include(u => u.Photos).OrderByDescending(u => u.LastActive).AsQueryable();
                users = users.Where(u => u.Id != request.CurrentUserId);
                users = users.Where(u => u.Gender.ToLower().Trim() == request.Gender.ToLower().Trim());



                if (request.Likees)
                {
                    var usersILiked = _applicationContext.Likes.Where(l => l.LikerId == request.CurrentUserId).Select(i => i.LikeeId);
                    users = users.Where(u => usersILiked.Contains(u.Id));
                }

                if (request.Likers)
                {
                    var usersThatLikedMe = _applicationContext.Likes.Where(l => l.LikeeId == request.CurrentUserId).Select(i => i.LikerId);
                    users = users.Where(u => usersThatLikedMe.Contains(u.Id));
                }



                if (request.MinAge != 18 || request.MaxAge != 99)
                {
                    var minDob = DateTime.Today.AddYears(-request.MaxAge - 1);
                    var maxDob = DateTime.Today.AddYears(-request.MinAge);
                    users = users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
                }

                if (!string.IsNullOrEmpty(request.OrderBy))
                {
                    users = request.OrderBy switch
                    {
                        "created" => users.OrderByDescending(u => u.Created),
                        _ => users.OrderByDescending(u => u.LastActive),
                    };
                }

                var pagedList = await PagedList<User>.CreateAsync(users, request.PageNumber, request.PageSize);

                var usersVm = _mapper.Map<IEnumerable<UserVm>>(pagedList);
                var ret = new UserVMPagination(pagedList.CurrentPage, pagedList.TotalPages, pagedList.PageSize, pagedList.TotalCount, usersVm);

                return ret;
            }
        }
    }
}
