namespace ExlaqiNasiri.Application.DTOs.Lesson
{
    public class LessonFilterDto
    {
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }

        public Guid? LessonFieldId { get; set; }
    }
}
