namespace BlogAPI.DTO.Article
{
    public class UpdateArticleDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; } = null;
        public string? Content { get; set; } = null;
        public int? ThemeId { get; set; } = null;
    }
}
