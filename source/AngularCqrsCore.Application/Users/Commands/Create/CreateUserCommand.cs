using System;
using System.Collections.Generic;
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
    public class CreateUserCommand : IRequest<CreateUserDto>, IMapFrom<User>
    {
        #region [ PROPS ]

        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Senha deve conter entre 4 e 15 caracteres")]
        public string Password { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<UserPhotos> Photos { get; set; }

        #endregion

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserDto>
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

            public async Task<CreateUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<User>(request);

                var userExists = await _userRepository.GetByLoginAsync(user.Login);

                if (userExists is object)
                    throw new Exception("Login já existe na base");

                await _applicationDbContext.User.AddAsync(user, cancellationToken);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return _mapper.Map<CreateUserDto>(user);
            }
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserCommand, User>()
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password.Crypt()))
                .ForMember(d => d.Login, opt => opt.MapFrom(s => s.Login.ToLower()))
                .ForMember(d => d.Photos, opt => opt.MapFrom(s => s.Photos))
                ;
        }
    }

    public class CreateUserDto : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<UserPhotos> Photos { get; set; }
    }


    public class UserPhotos : IMapFrom<Photo>
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserPhotos, Photo>()
                .ForMember(d => d.Url, opt => opt.MapFrom(s => s.Url))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.IsMain, opt => opt.MapFrom(s => s.IsMain))
                ;
        }
    }
}
