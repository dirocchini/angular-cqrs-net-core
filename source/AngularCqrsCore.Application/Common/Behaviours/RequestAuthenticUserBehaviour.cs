using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Behaviours
{
    public class RequestAuthenticUserBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IApplicationDbContext _context;

        public RequestAuthenticUserBehaviour(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is CheckUserValidation)
            {
                var prop = (request as CheckUserValidation).TokenUserId;
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == Convert.ToInt32(prop));

                if ((/*wants to change prop from this user*/ request as CheckUserValidation).Id != /*but token is configured for this user*/ user.Id)
                    throw new UnauthorizedAccessException();
            }

            return await next();
        }
    }
}
