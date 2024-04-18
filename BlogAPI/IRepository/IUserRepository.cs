using BlogAPI.Models;

namespace BlogAPI.IRepository
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User user);
        public Task<List<User>> GetAllAsync();
        public Task<User> GetUserByAppUserIdAsync(string appUserId);
        public Task DeleteAsync(int id);
    }
}
