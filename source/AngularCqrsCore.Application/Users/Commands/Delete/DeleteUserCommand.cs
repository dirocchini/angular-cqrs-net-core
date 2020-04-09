using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Interfaces;
using MediatR;

namespace Application.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }




        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
        {
            private readonly IUserRepository _userRepository;
            private readonly IApplicationDbContext _applicationDbContext;

            public DeleteUserCommandHandler(IUserRepository userRepository, IApplicationDbContext applicationDbContext)
            {
                _userRepository = userRepository;
                _applicationDbContext = applicationDbContext;
            }


            public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(request.UserId);

                if ( user == null) return false;

                _applicationDbContext.Users.Remove(user);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
    }
}
