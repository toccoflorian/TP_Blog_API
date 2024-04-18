using BlogAPI.DTO.Comment;
using BlogAPI.Models;

namespace BlogAPI.IRepository
{
    public interface ICommentRepository
    {
        public Task CreateAsync(CreateCommentDTO createCommentDTO);
        public Task<List<Comment>> GetByArticleAsync(int articleId);
    }
}
