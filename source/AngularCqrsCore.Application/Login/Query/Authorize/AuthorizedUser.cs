using Application.Common.Mappings;
using Application.Users.Commands.Create;
using AutoMapper;
using Domain.Entities;
using SharedOps;

namespace Application.Login.Query.Authorize
{
    public class AuthorizedUser : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
    }
}