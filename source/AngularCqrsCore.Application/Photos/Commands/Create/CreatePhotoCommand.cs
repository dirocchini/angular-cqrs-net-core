using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedOps;

namespace Application.Photos.Commands.Create
{
    public class CreatePhotoCommand : CheckUserValidation, IRequest<bool>
    {

        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public int UserId { get; set; }
        public DateTime DateAdded { get; set; }
        public IFormFile File { get; set; }


        public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, bool>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IOptions<CloudinarySettings> _options;

            public CreatePhotoCommandHandler(IApplicationDbContext applicationDbContext, IOptions<CloudinarySettings> options)
            {
                _applicationDbContext = applicationDbContext;
                _options = options;
            }


            public async Task<bool> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
            {
                var user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

                _applicationDbContext.Photos.Authenticate(_options.Value.CloudName, _options.Value.ApiKey, _options.Value.ApiSecrets);


                throw new Exception();
            }
        }

        public int TokenUserId { get; set; }
        public int Id { get; set; }
    }
}