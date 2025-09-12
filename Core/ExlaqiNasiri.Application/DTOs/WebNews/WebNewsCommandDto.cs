using Microsoft.AspNetCore.Http;

namespace ExlaqiNasiri.Application.DTOs.WebNews
{
    public class WebNewsCommandDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public IFormFile Image { get; set; }
    }
}
