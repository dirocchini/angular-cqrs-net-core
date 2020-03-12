using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Users.Queries.GetAll
{
    public class UserDto : IMapFrom<User>
    {
        public string Name { get; set; }
        public string Login { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Login, opt => opt.MapFrom(s => s.Login));
        }
    }
}
