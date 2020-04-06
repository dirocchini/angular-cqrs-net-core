using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Photos.Queries.GetMain
{
    public class GetMainPhotoQuery : IRequest<GetMainPhotoDto>
    {
        public class GetMainPhotoQueryHandler : IRequestHandler<GetMainPhotoQuery, GetMainPhotoDto>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetMainPhotoQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }
            public Task<GetMainPhotoDto> Handle(GetMainPhotoQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }

    public class GetMainPhotoDto
    {
    }
}
