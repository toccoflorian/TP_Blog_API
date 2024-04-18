using BlogAPI.Models;
using System.Security.Claims;

namespace BlogAPI.DTO.Comment
{
    public class CreateCommentDTO
    {
        public string Content { get; set; }
        public int ArticleId { get; set; }
        public int? UserId { get; set; }
    }
}
