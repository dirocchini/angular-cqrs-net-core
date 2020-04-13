using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Interfaces;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using SharedOps;

namespace Application.Photos.Commands.Create
{
    public class CreatePhotoCommand : CheckUserValidation, IRequest<PhotoDto>
    {

        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public int UserId { get; set; }
        public DateTime DateAdded { get; set; }
        public IFormFile File { get; set; }


        public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, PhotoDto>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IOptions<CloudinarySettings> _options;
            private readonly IPhotoRepository _photoRepository;
            private readonly IMapper _mapper;

            public CreatePhotoCommandHandler(IApplicationDbContext applicationDbContext, IOptions<CloudinarySettings> options, IPhotoRepository photoRepository, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _options = options;
                _photoRepository = photoRepository;
                _mapper = mapper;
            }


            public async Task<PhotoDto> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
            {
                var user = await _applicationDbContext.User.Include(u => u.Photos).FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

                _photoRepository.Authenticate(_options.Value.CloudName, _options.Value.ApiKey, _options.Value.ApiSecrets);
                var url = _photoRepository.SavePhoto(request.File.FileName, request.File.OpenReadStream());

                var photo = new Photo();

                if (!user.Photos.ToList().Any(p => p.IsMain))
                    photo.IsMain = true;

                if (url != null)
                {
                    photo.Url = url.Value.url;
                    photo.PublicId = url.Value.publicId;
                }

                user.Photos.Add(photo);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return  _mapper.Map<PhotoDto>(photo);
            }
        }

        public int TokenUserId { get; set; }
        public int Id { get; set; }
    }
}