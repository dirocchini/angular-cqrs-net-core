﻿using Domain.Common;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
