namespace ExlaqiNasiri.Application.DTOs.Hadith
{
    public class HadithFilterDto
    {
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public Guid? CategoryId { get; set; }
        public string? Source { get; set; }

    }
}
