using BlogAPI.Const;
using BlogAPI.DTO.Article;
using BlogAPI.Helpers.DTOHelper;
using BlogAPI.IRepository;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly Context _context;
        public ArticleRepository(Context context)
        {
            this._context = context;
        }

        public async Task<int> GetUserIdOfArticle(int articleId)
        {
            return (await this._context.Articles.FindAsync(articleId)).UserId;
        }


        public async Task<List<GetArticleDTO>> GetAllAsync()
        {
            List<Article> articles = await this._context.Articles
                .Include(a => a.Author)
                .Include(a => a.Theme)
                .ToListAsync();
            return await DTOHelper.GetArticleDTOMapToList(articles);
        }

        public async Task<List<GetArticleDTO>> GetAllOrderByAuthorAscendantAsync()
        {
            List<Article> articles = await this._context.Articles
                .Include(a => a.Author)
                .Include(a => a.Theme)
                .OrderBy(a => a.Author)
                .ToListAsync();
            return await DTOHelper.GetArticleDTOMapToList(articles);
        }

        public async Task<List<GetArticleDTO>> GetAllOrderByAuthorDescendantAsync()
        {
            List<Article> articles = await this._context.Articles
                .Include(a => a.Author)
                .Include(a => a.Theme)
                .OrderByDescending(a => a.Author)
                .ToListAsync();
            return await DTOHelper.GetArticleDTOMapToList(articles);
        }

        public async Task<List<GetArticleDTO>> GetAllOrderByThemeAscendantAsync()
        {
            List<Article> articles = await this._context.Articles
                .Include(a => a.Author)
                .Include(a => a.Theme)
                .OrderBy(a => a.Theme)
                .ToListAsync();
            return await DTOHelper.GetArticleDTOMapToList(articles);
        }

        public async Task<List<GetArticleDTO>> GetAllOrderByThemeDescendantAsync()
        {
            List<Article> articles = await this._context.Articles
                .Include(a => a.Author)
                .Include(a => a.Theme)
                .OrderByDescending(a => a.Theme)
                .ToListAsync();
            return await DTOHelper.GetArticleDTOMapToList(articles);
        }

        public async Task<List<GetArticleDTO>> GetByMySelfAsync(int userId)
        {
            List<Article> articles = await this._context.Articles
                .Where(a => a.UserId == userId)
                .Include(a => a.Author)
                .Include(a => a.Theme)
                .ToListAsync();
            return await DTOHelper.GetArticleDTOMapToList(articles);
        }

        public async Task<List<GetArticleDTO>> GetByAuthorAsync(int authorId)
        {
            List<Article> articles = await this._context.Articles
                .Where(a => a.UserId == authorId)
                .Include(a => a.Author)
                .Include(a => a.Theme)
                .ToListAsync();
            return await DTOHelper.GetArticleDTOMapToList(articles);
        }

        public async Task<List<GetArticleDTO>> GetByThemeAsync(int themeId)
        {
            List<Article> articles = await this._context.Articles
                .Where(a => a.ThemeId == themeId)
                .Include(a => a.Theme)
                .Include(a => a.Author)
                .ToListAsync();
            return await DTOHelper.GetArticleDTOMapToList(articles);
        }

        public async Task<GetArticleDTO?> GetOneByIdAsync(int articleId)
        {
            Article? article = await this._context.Articles
                .Include(a => a.Author)
                .Include(a => a.Theme)
                .FirstOrDefaultAsync(a => a.Id == articleId);
            if(article != null)
            {
                return await DTOHelper.GetArticleDTOMap(article);
            }
            return null;
        }

        public async Task<GetArticleWithAllInformationsDTO> GetOneByIdWithInformationsAsync(int articleId)
        {
            Article? article = await this._context.Articles
                .Include(a => a.Author)
                .Include(a => a.Theme)
                .Include(a => a.Comments)
                .ThenInclude(c => c.Author)
                .FirstOrDefaultAsync(a => a.Id == articleId);

            if (article == null)
            {
                return await DTOHelper.GetArticleWithAllInformationsDTOMap(article);
            }
            return null;
        }

        public async Task UpdateAsync(UpdateArticleDTO updateArticleDTO)
        {
            Article? article = await this._context.Articles.FindAsync(updateArticleDTO.Id);
            if(article == null)
            {
                throw new Exception();
            }
            if(updateArticleDTO.Title != null && updateArticleDTO.Content != "string")
            {
                article.Title = updateArticleDTO.Title;
            }
            if(updateArticleDTO.Content != null && updateArticleDTO.Content != "string")
            {
                article.Content = updateArticleDTO.Content;
            }
            if(updateArticleDTO.ThemeId != 0 && updateArticleDTO.ThemeId != null)
            {
                article.ThemeId = updateArticleDTO.ThemeId;
            }
            article.UpdatedDate = DateTime.Now;
            await this._context.SaveChangesAsync();
        }

        public async Task<string> CreateAsync(CreateArticleDTO articleDTO)
        {
            Article article = new Article
            {
                Title = articleDTO.Title,
                CreatedDate = DateTime.Now,
                UpdatedDate = null,
                Content = articleDTO.Content,
                UserId = articleDTO.UserId,
                ThemeId = articleDTO.ThemeId,
                Priority = PRIORITYEnum.Normale
            };
            await this._context.Articles.AddAsync(article);
            await this._context.SaveChangesAsync();
            return article.Id.ToString();
        }

        public async Task DeleteAsync(int articleId)
        {
            Article? article = await this._context.Articles.Include(a => a.Comments).FirstOrDefaultAsync(a => a.Id == articleId);
            if (article != null)
            {
                foreach (Comment comment in article.Comments)
                {
                    this._context.Comments.Remove(comment);
                }
            }
            this._context.Articles.Remove(article);
            await this._context.SaveChangesAsync();
        }

        public async Task UpgradePriorityAsync(int articleId)
        {
            Article? article = await this._context.Articles.FindAsync(articleId);
            if (article != null) 
            {
                article.Priority = PRIORITYEnum.Haute;
                await this._context.SaveChangesAsync();
            }
        }

        public async Task DowngradePriorityAsync(int articleId)
        {
            Article? article = await this._context.Articles.FindAsync(articleId);
            if (article != null)
            {
                article.Priority = PRIORITYEnum.Normale;
                await this._context.SaveChangesAsync();
            }
        }
    }
}
