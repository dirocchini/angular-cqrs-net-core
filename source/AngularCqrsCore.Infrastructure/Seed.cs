using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Domain.Entities;
using Newtonsoft.Json;
using SharedOps;

namespace Persistence
{
    public class Seed
    {
        public static void SeedUsers(AngularCoreContext context)
        {
            if (context.Users.Count() >= 2) return;

            var json = System.IO.File.ReadAllText("../AngularCqrsCore.Infrastructure/DataSeed/users.json");
            var users = JsonConvert.DeserializeObject<List<User>>(json);
            users.ForEach(u =>
            {
                u.Password = u.Password.Crypt();
                u.Login = u.Login.ToLower().Trim();
            });
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
