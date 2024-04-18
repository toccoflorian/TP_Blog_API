using BlogAPI.Const;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        [DeleteBehavior(DeleteBehavior.Cascade)]
        public List<Article>? Articles { get; set; }
        public List<Comment>? Comments { get; set; }

    }
}
