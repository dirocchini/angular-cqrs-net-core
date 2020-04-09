using Application.Common.Interfaces;
using Application.Users.Queries.GetAll;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Messages.Queries.GetMessagesForUser
{
    public class GetMessagesForUserQuery : IRequest<GetMessageForUserPagination>
    {
        public int CurrentUserId { get; set; }
        private const int MaxPageSize = 6;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public string MessageContainer { get; set; } = "Unread";



        public class GetMessagesForUserQueryHandler : IRequestHandler<GetMessagesForUserQuery, GetMessageForUserPagination>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetMessagesForUserQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper )
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }


            public async Task<GetMessageForUserPagination> Handle(GetMessagesForUserQuery request, CancellationToken cancellationToken)
            {
                if (request.PageSize > MaxPageSize || request.PageSize == 0)
                    request.PageSize = MaxPageSize;

                var messages = _applicationDbContext.Messages
                    .Include(u => u.Sender).ThenInclude(p => p.Photos)
                    .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                    .AsQueryable();

                messages = (request.MessageContainer.ToLower().Trim()) switch
                {
                    "inbox" => messages.Where(u => u.RecipientId == request.CurrentUserId),
                    "outbox" => messages.Where(u => u.SenderId == request.CurrentUserId),
                    _ => messages.Where(u => u.RecipientId == request.CurrentUserId && u.IsRead == false), //not read messsages
                };

                messages = messages.OrderByDescending(m => m.MessageSent);

                var pagedList = await PagedList<Message>.CreateAsync(messages, request.PageNumber, request.PageSize);

                var messagesDto = _mapper.Map<IEnumerable<GetMessagesForUserDto>>(pagedList);
                var ret = new GetMessageForUserPagination(pagedList.CurrentPage, pagedList.TotalPages, pagedList.PageSize, pagedList.TotalCount, messagesDto);
                return ret;
            }
        }
    }
}
