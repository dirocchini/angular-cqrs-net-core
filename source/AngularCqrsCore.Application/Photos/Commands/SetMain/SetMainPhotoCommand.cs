using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Photos.Commands.SetMain
{
    public class SetMainPhotoCommand : IRequest<MainPhotoDto>
    {
        public int PhotoId { get; set; }
        public int UserId { get; set; }

        public class SetMainPhotoCommandHandler : IRequestHandler<SetMainPhotoCommand, MainPhotoDto>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public SetMainPhotoCommandHandler(IApplicationDbContext _applicationDbContext, IMapper mapper)
            {
                this._applicationDbContext = _applicationDbContext;
                _mapper = mapper;
            }
            public async Task<MainPhotoDto> Handle(SetMainPhotoCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var photo = await _applicationDbContext.Photos.FirstOrDefaultAsync(p => p.Id == request.PhotoId,
                        cancellationToken);

                    int userId = request.UserId;

                    var photos = _applicationDbContext.Photos.Where(p => p.IsMain && p.UserId == userId).ToList();
                    photos.ForEach(p=>p.IsMain = false);

                    photo.IsMain = true;

                    await _applicationDbContext.SaveChangesAsync(cancellationToken);

                    return _mapper.Map<MainPhotoDto>(photo);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
