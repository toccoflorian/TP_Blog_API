using BlogAPI.Const;

namespace BlogAPI.DTO.Article
{
    public class GetArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Theme { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string AuthorName { get; set; }
        public string Priority { get; set; }
    }
}
