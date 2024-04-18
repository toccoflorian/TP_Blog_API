using BlogAPI.Const;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Content { get; set; }

        
        public int UserId { get; set; }
        public User Author { get; set; }
        public int? ThemeId { get; set; }
        public Theme? Theme { get; set; }
        public List<Comment>? Comments { get; set; }
        public PRIORITYEnum Priority { get; set; }

    }
}
