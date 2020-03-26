using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Photos.Commands.Create
{
    public class CreatePhotoCommand : IRequest<bool>
    {
        private readonly IApplicationDbContext _context;
        public string Name { get; set; }


        public CreatePhotoCommand(IApplicationDbContext context)
        {
            _context = context;
        }

        public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, bool>
        {
            public Task<bool> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
