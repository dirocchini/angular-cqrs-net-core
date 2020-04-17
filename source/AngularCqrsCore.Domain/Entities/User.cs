using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public partial class User : IdentityUser<int>
    {
        
        private string name { get; set; }
        public string Name { get { return !string.IsNullOrEmpty(name) ? name : UserName; } set { name = value; } }

        private string login { get; set; }
        public string Login { get { return !string.IsNullOrEmpty(login) ? login : UserName; } set { login = value; } }
        public string Password { get; set; }


        public string Gender { get; set; }
        public int Age
        {
            get
            {
                try
                {
                    return (DateTime.Today.Year - DateOfBirth.Year);
                }
                catch (Exception)
                {
                    return 55;
                }
            }
        }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Like> Likers { get; set; }
        public ICollection<Like> Likees { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }


        public int CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
