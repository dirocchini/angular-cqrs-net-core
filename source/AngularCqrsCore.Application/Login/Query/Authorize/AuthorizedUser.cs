using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Login.Query.Authorize
{
    public class AuthorizedUser : IMapFrom<User>
    {
        #region [ PROPS ]

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Created { get; set; }

        public ICollection<UserPhotos> Photos { get; set; }


        #endregion


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



    public class UserPhotos : IMapFrom<Photo>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
    }
}
