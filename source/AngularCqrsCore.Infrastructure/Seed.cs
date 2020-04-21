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
        public static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (userManager.Users.Count() >= 2) return;

            var json = System.IO.File.ReadAllText("../AngularCqrsCore.Infrastructure/DataSeed/users.json");
            var users = JsonConvert.DeserializeObject<List<User>>(json);

            var roles = new List<Role> {
                 new Role{Name = "Member"},
                 new Role{Name = "Admin"},
                 new Role{Name = "Moderator"},
                 new Role{Name = "VIP"}
            };

            foreach (var role in roles)
                roleManager.CreateAsync(role).Wait();

            foreach (var user in users)
            {
                user.Photos.SingleOrDefault().IsApproved = true;
                userManager.CreateAsync(user, "password").Wait();
                userManager.AddToRoleAsync(user, "Member").Wait();
            }

            var adminUser = new User
            {
                UserName = "admin",
                Login = "admin",
                Password = "password"
            };

            var result = userManager.CreateAsync(adminUser, "password").Result;
            if (result.Succeeded)
            {
                var admin = userManager.FindByNameAsync("admin").Result;
                userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" }).Wait();
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
