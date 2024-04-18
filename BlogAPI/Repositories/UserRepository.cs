using BlogAPI.IRepository;
using BlogAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace BlogAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        public UserRepository(Context context, UserManager<AppUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }
        public async Task AddUserAsync(User user)
        {
                this._context.Users.Add(user);
                this._context.SaveChanges();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await this._context.Users.ToListAsync();
        }

        public async Task<User> GetUserByAppUserIdAsync(string appUserId)
        {
            return await this._context.Users.FirstOrDefaultAsync(u => u.AppUserId == appUserId);
        }

        public async Task DeleteAsync(int userId)
        {
            User? user = await this._context.Users.FirstOrDefaultAsync(c => c.Id == userId);

            this._context.Users.Remove(user);
            await this._context.SaveChangesAsync();
            await this._userManager.DeleteAsync(await this._userManager.FindByIdAsync(user.AppUserId));
        }
    }
}
