using Microsoft.AspNetCore.Http;

namespace ExlaqiNasiri.Application.DTOs.Lesson
{
    public class LessonCreateDto
    {
        public Guid LessonFieldId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public IFormFile Image { get; set; }
    }
}
