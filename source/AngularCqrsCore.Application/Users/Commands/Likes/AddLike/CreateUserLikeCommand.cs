using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.Likes.AddLike
{
    public class CreateUserLikeCommand : IRequest<bool>
    {
        public CreateUserLikeCommand(int id, int recipientId)
        {
            Id = id;
            RecipientId = recipientId;
        }

        public int Id { get; }
        public int RecipientId { get; }





        public class CreateUserLikeCommandHandler : IRequestHandler<CreateUserLikeCommand, bool>
        {
            public IApplicationDbContext _applicationDbContext { get; }

            public CreateUserLikeCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }


            public async Task<bool> Handle(CreateUserLikeCommand request, CancellationToken cancellationToken)
            {
                var alreadyLiked = _applicationDbContext.Likes.FirstOrDefault(l => l.LikeeId == request.RecipientId && l.LikerId == request.Id);

                if (alreadyLiked != null)
                    return false;

                if(await _applicationDbContext.Users.FirstOrDefaultAsync(u=>u.Id == request.RecipientId)==null)
                    return false;

                var like = new Like()
                {
                    LikerId = request.Id,
                    LikeeId = request.RecipientId
                };

                _applicationDbContext.Likes.Add(like);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);


                return true;
            }
        }
    }
}
