using System;
using System.Collections;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public partial class User : AuditableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
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
    }
}
