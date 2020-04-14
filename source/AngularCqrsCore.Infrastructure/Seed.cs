using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SharedOps;

namespace Persistence
{
    public class Seed
    {
        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.Users.Count() >= 2) return;

            var json = System.IO.File.ReadAllText("../AngularCqrsCore.Infrastructure/DataSeed/users.json");
            var users = JsonConvert.DeserializeObject<List<User>>(json);
            foreach (var user in users)
            {
                userManager.CreateAsync(user, "password").Wait();
            }

            //users.ForEach(u =>
            //{
            //    userManager.CreateAsync(userManager )

            //    //u.Password = u.Password.Crypt();
            //    //u.Login = u.Login.ToLower().Trim();
            //});
        }
    }
}
