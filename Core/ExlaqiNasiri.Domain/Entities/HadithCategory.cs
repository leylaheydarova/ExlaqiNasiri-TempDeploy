using ExlaqiNasiri.Domain.Entities.BaseEntities;

namespace ExlaqiNasiri.Domain.Entities
{
    public class HadithCategory : BaseEntityWithDelete
    {
        public string CategoryName { get; set; }
        public ICollection<Hadith> Hadiths { get; set; }
    }
}
