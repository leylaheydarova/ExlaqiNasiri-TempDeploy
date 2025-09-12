using Microsoft.AspNetCore.Http;

namespace ExlaqiNasiri.Application.DTOs.Hadith
{
    public class HadithCreateDto
    {
        public Guid CategoryId { get; set; }
        public string HadithContext { get; set; }
        public string Text { get; set; }
        public IFormFile Image { get; set; }
        public string Source { get; set; }
    }
}
