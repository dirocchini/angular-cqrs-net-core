using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    class Ret {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public List<string> Roles { get; set; }
    }

    [Route("[controller]")]
    [ApiController]
    public class AdminController : BaseController
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AdminController(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userRoles = _applicationDbContext.UserRoles.ToList();
            var roles = _applicationDbContext.Roles.ToList();
            var allRoles = (from userRole in userRoles
                            join role in roles on userRole.RoleId equals role.Id
                            select new { RoleName = role.Name, UserId = userRole.UserId }).ToList();

            var rrr = allRoles.Where(r => r.UserId == 1).Select(role => new { role.RoleName }).ToList();

            var userList = await _applicationDbContext.User
                .OrderBy(u => u.UserName)
                .Select(user => new Ret
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Login = user.UserName
                }).ToListAsync();

            foreach (var user in userList)
            {
                var ownedRoles = allRoles.Where(r => r.UserId == user.Id).Select(role => new { RoleName = role.RoleName }).ToList();
                user.Roles = new List<string>();
                ownedRoles.ForEach(r => user.Roles.Add(r.RoleName));
            }

            return Ok(userList);
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photosForModeration")]
        public IActionResult GetPhotosForModeration()
        {
            return Ok("Admins or Moderators can see this");
        }
    }
}