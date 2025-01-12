namespace NewsFeedApp.Models
{
    public class News
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? PubDate { get; set; }
        public string? ArcticleLink { get; set; }
        public string? GUID { get; set; }
        public string? MediaContent { get; set; }

    }
}
