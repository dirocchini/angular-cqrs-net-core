using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Users.Queries.GetAll
{
    public class UserVm : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }


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
        //public ICollection<PhotoDto> Photos { get; set; }
        //public ICollection<int> Likers { get; set; }
        //public ICollection<int> Likees { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Login, opt => opt.MapFrom(s => s.Login))
                .ForMember(d => d.PhotoUrl, opt => opt.MapFrom(s => GetPhotoUrl(s.Photos)));
        }

        private string GetPhotoUrl(ICollection<Photo> photos)
        {
            return photos.FirstOrDefault(f => f.IsMain)?.Url ?? null;
        }
    }

    public class PhotoDto : IMapFrom<Photo>
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
    }
}
