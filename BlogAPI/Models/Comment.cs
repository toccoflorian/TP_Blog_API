using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Content { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int UserId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public User Author { get; set; }

    }
}
