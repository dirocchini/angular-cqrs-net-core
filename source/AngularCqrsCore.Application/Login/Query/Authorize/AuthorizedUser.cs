using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Login.Query.Authorize
{
    public class AuthorizedUser : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
    }
}