using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedOps;

namespace Application.Photos.Commands.Delete
{
    public class DeletePhotoCommand : IRequest<bool>
    {
        public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, bool>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IPhotoRepository _photoRepository;
            private readonly IOptions<CloudinarySettings> _options;

            public DeletePhotoCommandHandler(IApplicationDbContext applicationDbContext, IPhotoRepository photoRepository, IOptions<CloudinarySettings> options)
            {
                _applicationDbContext = applicationDbContext;
                _photoRepository = photoRepository;
                _options = options;
            }
            public async Task<bool> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
            {
                var photo = await _applicationDbContext.Photos.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == request.PhotoId, cancellationToken: cancellationToken);


                _applicationDbContext.Photos.Remove(photo);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);



                _photoRepository.Authenticate(_options.Value.CloudName, _options.Value.ApiKey, _options.Value.ApiSecrets);
                _photoRepository.DestroyPhoto(photo.PublicId);


                return true;
            }
        }

        public int PhotoId { get; set; }
    }
}
