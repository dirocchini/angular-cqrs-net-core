using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommand : IRequest<CreateMessageDto>
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public DateTime MessageSent { get; set; }
        public string Content { get; set; }

        public CreateMessageCommand()
        {
            MessageSent = DateTime.Now;
        }



        public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, CreateMessageDto>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public CreateMessageCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }


            public async Task<CreateMessageDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
            {
                var recipient = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RecipientId, cancellationToken);

                if (recipient == null)
                    return null;

                var message = _mapper.Map<Message>(request);
                message.Created = DateTime.Now;

                _applicationDbContext.Messages.Add(message);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                message = await _applicationDbContext.Messages
                    .Include(s => s.Sender).ThenInclude(s => s.Photos)
                    .Include(s => s.Recipient).ThenInclude(s => s.Photos)
                    .FirstOrDefaultAsync(m => m.Id == message.Id);


                return _mapper.Map<CreateMessageDto>(message);
            }
        }
    }
}
