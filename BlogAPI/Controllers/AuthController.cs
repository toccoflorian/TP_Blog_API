using BlogAPI.DTO.User;
using BlogAPI.IRepository;
using BlogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        public AuthController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserRepository userRepository,
            IAuthRepository authRepository)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._userRepository = userRepository;
            this._authRepository = authRepository;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <returns>string</returns>
        [HttpPost]
        public async Task<ActionResult<string>> Register(RegisterUserDTO registerUserDTO)
        {
            if (!Helpers.AgeCalculHelper.AgeCalculHelper.IsUserAdult(new DateTime(registerUserDTO.YearOfBirth, registerUserDTO.MonthOfBirth, registerUserDTO.DayOfBirth)))
                return BadRequest("Vous êtes trop jeune");

            if (registerUserDTO.Password != registerUserDTO.ConfirmPassword)
                return BadRequest("Le mot de passe et la confirmation du mot de passe doivent être identiques !");

            try
            {
                AppUser appUser = new AppUser { UserName = registerUserDTO.Email, NormalizedUserName = registerUserDTO.Email.ToUpper() };
                IdentityResult? result = await this._userManager.CreateAsync(appUser, registerUserDTO.Password);

                await this._userRepository.AddUserAsync(
                    new User
                    {
                        Name = registerUserDTO.Name,
                        BirthDate = new DateOnly(registerUserDTO.YearOfBirth, registerUserDTO.MonthOfBirth, registerUserDTO.DayOfBirth),
                        AppUserId = appUser.Id
                    });

                return Ok("OK");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.InnerException.Message);
            }

        }


    }
}
