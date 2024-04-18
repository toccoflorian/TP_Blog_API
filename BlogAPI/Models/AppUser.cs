using Microsoft.AspNetCore.Identity;

namespace BlogAPI.Models
{
    public class AppUser : IdentityUser
    {
        public User User { get; set; }
    }
}
