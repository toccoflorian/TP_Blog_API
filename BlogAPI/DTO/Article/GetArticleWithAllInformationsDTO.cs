using BlogAPI.Const;
using BlogAPI.DTO.Comment;

namespace BlogAPI.DTO.Article
{
    public class GetArticleWithAllInformationsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Theme { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string AuthorName { get; set; }
        public DateOnly AuthorBirthDate { get; set; }
        public string Priority { get; set; }
        public List<GetCommentWithInformationsDTO>? Comments { get; set; }
    }
}
