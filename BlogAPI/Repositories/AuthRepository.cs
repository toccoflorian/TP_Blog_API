using BlogAPI.DTO.User;
using BlogAPI.IRepository;
using BlogAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BlogAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;
        public AuthRepository(
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IUserRepository userRepository)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._userRepository = userRepository;
        }

        public async void RegisterAsync(RegisterUserDTO registerUserDTO)
        {
            AppUser appUser = new AppUser { UserName = registerUserDTO.Email, NormalizedUserName = registerUserDTO.Email.ToUpper() };
            IdentityResult? result = await this._userManager.CreateAsync(appUser, registerUserDTO.Password);

            if (result.Succeeded)
            {
                if (!await this._roleManager.RoleExistsAsync(Const.ROLE.USER))
                {
                    IdentityRole role = new IdentityRole { Name = Const.ROLE.USER };
                    await this._roleManager.CreateAsync(role);
                }
                IdentityResult res = await this._userManager.AddToRoleAsync(appUser, Const.ROLE.USER);
                await this._userRepository.AddUserAsync(
                    new User
                    {
                        Name = registerUserDTO.Name,
                        BirthDate = new DateOnly
                        (
                            registerUserDTO.YearOfBirth,
                            registerUserDTO.MonthOfBirth,
                            registerUserDTO.DayOfBirth
                        ),
                        AppUserId = appUser.Id
                    }
                    );
            }

            
        }
    }
}
