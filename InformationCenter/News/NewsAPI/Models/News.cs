using System;

namespace NewsAPI.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public string FileUrl { get; set; }
    }
}
