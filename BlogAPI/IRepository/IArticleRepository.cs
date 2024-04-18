using BlogAPI.DTO.Article;
using BlogAPI.Models;

namespace BlogAPI.IRepository
{
    public interface IArticleRepository
    {
        public Task<List<GetArticleDTO>> GetAllAsync();

        public Task<List<GetArticleDTO>> GetAllOrderByAuthorAscendantAsync();
        public Task<List<GetArticleDTO>> GetAllOrderByAuthorDescendantAsync();
        public Task<List<GetArticleDTO>> GetAllOrderByThemeAscendantAsync();
        public Task<List<GetArticleDTO>> GetAllOrderByThemeDescendantAsync();
        public Task<List<GetArticleDTO>> GetByMySelfAsync(int userId);
        public Task<List<GetArticleDTO>> GetByAuthorAsync(int authorId);
        public Task<List<GetArticleDTO>> GetByThemeAsync(int themeId);
        public Task<GetArticleDTO?> GetOneByIdAsync(int articleId);
        public Task<GetArticleWithAllInformationsDTO> GetOneByIdWithInformationsAsync(int articleId);


        public Task<string> CreateAsync(CreateArticleDTO articleDTO);
        public Task UpdateAsync(UpdateArticleDTO updateArticleDTO);
        public Task DeleteAsync(int articleId);

        public Task UpgradePriorityAsync(int articleId);
        public Task DowngradePriorityAsync(int articleId);

        public Task<int> GetUserIdOfArticle(int articleId);
    }
}
