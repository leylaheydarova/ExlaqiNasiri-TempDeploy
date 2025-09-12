using Microsoft.AspNetCore.Http;

namespace ExlaqiNasiri.Application.DTOs.Lesson
{
    public class LessonUpdateDto
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public IFormFile Image { get; set; }
    }
}

