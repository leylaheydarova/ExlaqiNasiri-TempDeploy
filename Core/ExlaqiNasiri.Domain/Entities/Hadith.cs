using ExlaqiNasiri.Domain.Entities.BaseEntities;

namespace ExlaqiNasiri.Domain.Entities
{
    public class Hadith : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public string HadithContext { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public string Source { get; set; }
        public HadithCategory Category { get; set; }

    }
}
