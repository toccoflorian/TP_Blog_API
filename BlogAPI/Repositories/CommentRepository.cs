using BlogAPI.DTO.Comment;
using BlogAPI.IRepository;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogAPI.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly Context _context;
        public CommentRepository(Context context)
        {
            this._context = context;
        }

        public async Task CreateAsync(CreateCommentDTO createCommentDTO)
        {
            Comment comment = new Comment
            {
                Content = createCommentDTO.Content,
                CreatedDate = DateTime.UtcNow,
                ArticleId = createCommentDTO.ArticleId,
                UserId = (int)createCommentDTO.UserId
            };
            await this._context.Comments.AddAsync(comment);
            await this._context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetByArticleAsync(int articleId)
        {
            return await this._context.Comments.Where(c => c.ArticleId == articleId).ToListAsync();
        }
    }
}
