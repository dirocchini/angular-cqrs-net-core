using System.Collections.Generic;
using System.Linq;
using Application.Common.Mappings;
using Application.Users.Queries.Get;
using AutoMapper;
using Domain.Entities;

namespace Application.Login.Query.Authorize
{
    public class AuthorizedUser : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string PhotoUrl { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, AuthorizedUser>()
                .ForMember(d => d.PhotoUrl, opt => opt.MapFrom(s => GetPhotoUrl(s.Photos)));
        }

        private string GetPhotoUrl(IEnumerable<Photo> photos)
        {
            return photos.FirstOrDefault(f => f.IsMain)?.Url ?? null;
        }
    }
}