using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Api.Controllers.Dto;

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
        private readonly UserManager<User> _userManager;

        public AdminController(IApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
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

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles (string userName, RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames;

            selectedRoles = selectedRoles ?? Array.Empty<string>();

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to remove roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

    }
}