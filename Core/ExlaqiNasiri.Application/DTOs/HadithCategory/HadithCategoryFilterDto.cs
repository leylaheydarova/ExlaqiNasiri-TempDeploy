namespace ExlaqiNasiri.Application.DTOs.HadithCategory
{
    public class HadithCategoryFilterDto
    {
        public DateTime? CreatedFrom { get; set; } // bu tarixden sonra yarananlar
        public DateTime? CreatedTo { get; set; } //bu tarixe qeder yarananlar
        public bool? isDeleted { get; set; }
    }
}
