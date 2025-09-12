namespace ExlaqiNasiri.Application.DTOs.LessonField
{
    public class LessonFieldFilterDto
    {
        public DateTime? CreatedFrom { get; set; } // bu tarixden sonra yarananlar
        public DateTime? CreatedTo { get; set; } //bu tarixe qeder yarananlar
        public bool? isDeleted { get; set; }
    }
}
