using BlogAPI.Const;
using BlogAPI.IRepository;
using BlogAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = ROLE.ADMIN)]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Context _context;
        public AdminController(IUserRepository userRepository, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, Context context)
        {
            this._userRepository = userRepository;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUser()
        {
            return Ok(await this._userRepository.GetAllAsync());
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            await this._userRepository.DeleteAsync(userId);
            return Ok();
        }

        [HttpPut]
        public async Task UpgradeUserToAdminRole(int userId)
        {
            await this._roleManager.CreateAsync(new IdentityRole            // Desolé c'est moche mais il est 17h30 et on dois rendre...
            {
                Name = ROLE.ADMIN,
                NormalizedName = ROLE.ADMIN
            });
            await this._roleManager.CreateAsync(new IdentityRole
            {
                Name = ROLE.USER,
                NormalizedName = ROLE.USER
            });
            AppUser? appUser = (await this._context.Users.Include(u => u.AppUser).FirstOrDefaultAsync(u => u.Id == userId)).AppUser;
            await this._userManager.AddToRoleAsync(appUser, ROLE.ADMIN);
        }

        [HttpPut]
        public async Task DowngradeUserToUserRole(int userId)
        {
            await this._roleManager.CreateAsync(new IdentityRole            // Desolé c'est moche mais il est 17h30 et on dois rendre...
            {
                Name = ROLE.ADMIN,
                NormalizedName = ROLE.ADMIN
            });
            await this._roleManager.CreateAsync(new IdentityRole
            {
                Name = ROLE.USER,
                NormalizedName = ROLE.USER
            });
            AppUser? appUser = (await this._context.Users.Include(u => u.AppUser).FirstOrDefaultAsync(u => u.Id == userId)).AppUser;
            await this._userManager.AddToRoleAsync(appUser, ROLE.USER);
        }
    }
}
