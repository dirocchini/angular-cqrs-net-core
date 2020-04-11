using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Messages.Queries.GetMessageThread
{
    public class GetMessageThreadQuery:IRequest<List<GetMessageThreadDto>>
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }



        public class GetMessageThreadQueryHandler : IRequestHandler<GetMessageThreadQuery, List<GetMessageThreadDto>>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetMessageThreadQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }
            public async Task<List<GetMessageThreadDto>> Handle(GetMessageThreadQuery request, CancellationToken cancellationToken)
            {
                var messages = await _applicationDbContext.Messages
                    .Include(u => u.Sender).ThenInclude(m => m.Photos)
                    .Include(u => u.Recipient).ThenInclude(m => m.Photos)
                    .Where(m =>
                        m.RecipientId == request.SenderId && m.SenderId == request.RecipientId ||
                        m.RecipientId == request.RecipientId && m.SenderId == request.SenderId)
                    .OrderByDescending(m => m.MessageSent)
                    .ToListAsync();

                return _mapper.Map<List<GetMessageThreadDto>>(messages);
            }
        }
    }
}
