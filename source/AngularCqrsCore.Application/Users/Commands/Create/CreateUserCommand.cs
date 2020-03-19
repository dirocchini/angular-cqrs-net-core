using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using SharedOps;

namespace Application.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<int>, IMapFrom<User>
    {
        #region [ PROPS ]

        public string Name { get; set; }
        
        public string Email { get; set; }
        
        [Required]
        public string Login { get; set; }
        
        [Required]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Senha deve conter entre 4 e 15 caracteres")]
        public string Password { get; set; }

        #endregion

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;

            public CreateUserCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper, IUserRepository userRepository)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
                _userRepository = userRepository;
            }

            public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<User>(request);

                var userExists = await _userRepository.GetByLoginAsync(user.Login);

                if(userExists is object)
                    throw new Exception("Login já existe na base");

                await _applicationDbContext.Users.AddAsync(user, cancellationToken);
                return await _applicationDbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserCommand, User>()
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password.Crypt()))
                .ForMember(d => d.Login, opt => opt.MapFrom(s => s.Login.ToLower()));
        }
    }
}