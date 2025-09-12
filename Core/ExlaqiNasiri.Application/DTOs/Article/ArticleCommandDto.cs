using Microsoft.AspNetCore.Http;

namespace ExlaqiNasiri.Application.DTOs.Article
{
    public class ArticleCommandDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public IFormFile Image { get; set; }
    }
}
