using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Photos.Queries.Get
{
    public class GetPhotoQuery : IRequest<GetPhotoDto>
    {
        public int Id { get; set; }

        public class GetPhotoQueryHandler : IRequestHandler<GetPhotoQuery, GetPhotoDto>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetPhotoQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<GetPhotoDto> Handle(GetPhotoQuery request, CancellationToken cancellationToken)
            {
                var photo = await _applicationDbContext.Photos.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);
                return _mapper.Map<GetPhotoDto>(photo);
            }
        }
    }
}
