using System.Collections.Generic;

namespace Application.Users.Queries.GetAll
{
    public class UsersVm
    {
        public UsersVm(List<UserDto> users)
        {
            Users = users;
        }
        public List<UserDto> Users { get; set; }
    }
}