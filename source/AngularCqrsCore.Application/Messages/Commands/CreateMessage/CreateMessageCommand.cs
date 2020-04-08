using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommand : IRequest<string>
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public DateTime  MessageSent{ get; set; }
        public string Content { get; set; }

        public CreateMessageCommand()
        {
            MessageSent = DateTime.Now;
        }



        public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, string>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public CreateMessageCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }


            public async Task<string> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
            {
                var recipient = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RecipientId, cancellationToken);

                if (recipient == null)
                    return "Recipient Not Found";

                var message = _mapper.Map<Message>(request);



                return string.Empty;
            }
        }
    }
}
