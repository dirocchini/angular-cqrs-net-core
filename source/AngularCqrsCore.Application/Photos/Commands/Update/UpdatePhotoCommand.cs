using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Photos.Commands.Update
{
    public class UpdatePhotoCommand : IRequest<UpdatePhotoDto>, IMapFrom<Photo>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public int UserId { get; set; }


        public class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommand, UpdatePhotoDto>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public UpdatePhotoCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }
            public async Task<UpdatePhotoDto> Handle(UpdatePhotoCommand request, CancellationToken cancellationToken)
            {
                var photo = await _applicationDbContext.Photos.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
                
                if (photo != null)
                {
                    photo.LastModified = DateTime.Now;
                    photo.Description = string.IsNullOrEmpty(request.Description) ? photo.Description : request.Description;
                    photo.IsMain = request.IsMain;
                    await _applicationDbContext.SaveChangesAsync(cancellationToken);
                }

                return _mapper.Map<UpdatePhotoDto>(photo);
            }
        }
    }




    public class UpdatePhotoDto : IMapFrom<Photo>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
