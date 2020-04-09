using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Messages.Queries.GetMessage
{
    public class GetMessageQuery : IRequest<MessageDto>
    {
        public int MessageId { get; set; }


        public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, MessageDto>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetMessageQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<MessageDto> Handle(GetMessageQuery request, CancellationToken cancellationToken)
            {
                var message = await _applicationDbContext.Messages.FirstOrDefaultAsync(m => m.Id == request.MessageId, cancellationToken);

                if (message == null)
                    return null;

                return _mapper.Map<MessageDto>(message);
            }
        }
    }
}
