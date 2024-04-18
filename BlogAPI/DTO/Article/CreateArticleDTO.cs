namespace BlogAPI.DTO.Article
{
    public class CreateArticleDTO
    {
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int ThemeId { get; set; }
        public int PriorityId { get; set; }
    }
}
