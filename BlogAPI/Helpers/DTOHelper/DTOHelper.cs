using BlogAPI.Const;
using BlogAPI.DTO.Article;
using BlogAPI.DTO.Comment;
using BlogAPI.Models;

namespace BlogAPI.Helpers.DTOHelper
{
    public static class DTOHelper
    {
        public static async Task<List<GetArticleDTO>> GetArticleDTOMapToList(List<Article> articles)
        {
            List<GetArticleDTO> articlesDTO = new List<GetArticleDTO>();
            foreach (var article in articles)
            {
                articlesDTO.Add(new GetArticleDTO
                {
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content,
                    Theme = article.Theme.Name,
                    CreatedDate = article.CreatedDate,
                    UpdatedDate = article.UpdatedDate != null ? article.UpdatedDate : null,
                    AuthorName = article.Author.Name,
                    Priority = article.Priority.ToString(),
                });
            }
            return articlesDTO;
        }
    

        public static async Task<GetArticleDTO> GetArticleDTOMap(Article article)
        {
            return new GetArticleDTO
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Theme = article.Theme.Name,
                CreatedDate = article.CreatedDate,
                UpdatedDate = article.UpdatedDate != null ? article.UpdatedDate : null,
                AuthorName = article.Author.Name,
                Priority = article.Priority.ToString()
            };
        }

        public static async Task<GetArticleWithAllInformationsDTO> GetArticleWithAllInformationsDTOMap(Article article)
        {
            List<GetCommentWithInformationsDTO> commentsDTO = new List<GetCommentWithInformationsDTO>();
            foreach(Comment comment in article.Comments)
            {
                commentsDTO.Add(new GetCommentWithInformationsDTO
                {
                    Id = comment.Id,
                    CreatedDate = comment.CreatedDate,
                    UpdatedDate = comment.UpdatedDate != null ? comment.UpdatedDate : null,
                    Content = comment.Content,
                    AuthorName = comment.Author.Name,
                    AuthorId = comment.UserId
                });
            }
            return new GetArticleWithAllInformationsDTO
            {
                Id = article.Id, 
                Title = article.Title,
                Content = article.Content,
                Theme = article.Theme.Name,
                CreatedDate = article.CreatedDate,
                UpdatedDate = article.UpdatedDate != null ? article.UpdatedDate : null,
                AuthorName = article.Author.Name,
                AuthorBirthDate = article.Author.BirthDate,
                Priority = article.Priority.ToString(),
                Comments = commentsDTO.Count() > 0 ? commentsDTO : null
            };
        }
    }
}
